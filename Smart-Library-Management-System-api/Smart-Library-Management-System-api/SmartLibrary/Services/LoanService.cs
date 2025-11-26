using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;
using System.Globalization;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly ILoanRepository _loanRepo;
        private readonly IFineRepository _fineRepo;
        private readonly IFineService _fineService;

        
        private const int StudentLimit = 3;
        private const int FacultyLimit = 6;
        private const int DefaultLoanDaysStudent = 14;
        private const int DefaultLoanDaysFaculty = 60;

        public LoanService(
            IBookRepository bookRepo,
            IUserRepository userRepo,
            ILoanRepository loanRepo,
            IFineRepository fineRepo,
            IFineService fineService)
        {
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _loanRepo = loanRepo;
            _fineRepo = fineRepo;
            _fineService = fineService;
        }

       
        public async Task<Loan> BorrowBook(BorrowBookRequest request)
        {
            string isbn = null;

            var isbnProp = request.GetType().GetProperty("ISBN");
            if (isbnProp != null)
            {
                isbn = isbnProp.GetValue(request) as string;
            }

            if (string.IsNullOrWhiteSpace(isbn))
            {
                
                throw new InvalidOperationException("BorrowBookRequest must include ISBN. If you only have BookId, map BookId -> ISBN before calling the service.");
            }

            var book = await _bookRepo.GetBookByISBN(isbn);
            if (book == null) throw new InvalidOperationException("Book not found.");
            if (!book.IsAvailable) throw new InvalidOperationException("Book is not available.");

            var user = await _userRepo.GetUserById(request.UserId);
            if (user == null) throw new InvalidOperationException("User not found.");

            // check user's current active loans
            var activeCount = await _loanRepo.GetActiveLoans(request.UserId);

            int limit = user switch
            {
                Student _ => StudentLimit,
                Faculty _ => FacultyLimit,
                _ => 2
            };

            if (book.AvailableCopies >= 1)
            {
                throw new InvalidOperationException($"User has reached the borrowing limit ({limit}).");
            }


            // create loan
            var loan = new Loan
            {
                // If Loan has integer auto Id in your entity use that; else adjust
                UserId = request.UserId,
                ISBN = book.ISBN, // if your Loan entity stores ISBN as BookId; otherwise store book reference
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(user is Faculty ? DefaultLoanDaysFaculty : DefaultLoanDaysStudent)
            };

            await _loanRepo.AddLoan(loan);

            // decrement available copies
            book.AvailableCopies = Math.Max(0, book.AvailableCopies - 1);
            await _bookRepo.UpdateBook(book);

            return loan;
        }

        public async Task<Loan> ReturnBook(int loanId)
        {
            var loan = await _loanRepo.GetLoanById(loanId);
            if (loan == null) throw new InvalidOperationException("Loan not found.");
            if (loan.ReturnDate != null) throw new InvalidOperationException("Book already returned.");

            loan.ReturnDate = DateTime.UtcNow;
            await _loanRepo.UpdateLoan(loan);

            // increase available copies
            var book = await _bookRepo.GetBookByISBN(loan.ISBN);
            if (book != null)
            {
                book.AvailableCopies = book.AvailableCopies + 1;
                await _bookRepo.UpdateBook(book);
            }

            // overdue → create fine
            if (loan.ReturnDate > loan.DueDate)
            {
                var fineAmount = await _fineService.CalculateFine(loan.DueDate, loan.ReturnDate.Value);

                await _fineRepo.AddFine(new Fine
                {
                    LoanId = loan.LoanId,   // FIXED: your property is LoanId, not Id
                    Amount = fineAmount,
                    IsPaid = false,
                    IssuedAt = DateTime.UtcNow,
                });
            }

            return loan; // FIXED: always return a value
        }

        public async Task<List<Loan>> GetUserLoans(int userId)
        {
            return await _loanRepo.GetLoansByUserId(userId);
        }
    }
}
