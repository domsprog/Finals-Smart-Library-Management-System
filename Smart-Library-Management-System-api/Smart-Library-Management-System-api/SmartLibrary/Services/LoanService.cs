using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.LoanDTOs;
using SmartLibrary.DTOs.FineDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IFacultyRepository _facultyRepo;
        private readonly IFineService _fineService;

        public LoanService(
            ILoanRepository loanRepo,
            IBookRepository bookRepo,
            IStudentRepository studentRepo,
            IFacultyRepository facultyRepo,
            IFineService fineService)
        {
            _loanRepo = loanRepo;
            _bookRepo = bookRepo;
            _studentRepo = studentRepo;
            _facultyRepo = facultyRepo;
            _fineService = fineService;
        }

        public async Task<LoanResponseDTO> BorrowBookAsync(BorrowBookDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.ISBN) || string.IsNullOrWhiteSpace(dto.UserId))
                throw new ArgumentException("ISBN and UserId required");

            var book = await _bookRepo.GetBookByISBN(dto.ISBN);
            if (book == null) throw new InvalidOperationException("Book not found");
            if (book.AvailableCopies <= 0) throw new InvalidOperationException("No copies available");

            int limit = 2;
            int durationDays = 14;

            var student = await _studentRepo.GetStudentById(dto.UserId);
            var faculty = await _facultyRepo.GetFacultyById(dto.UserId);

            if (student != null)
            {
                limit = student.GetBorrowLimit();
                durationDays = student.GetBorrowDuration();
            }
            else if (faculty != null)
            {
                limit = faculty.GetBorrowLimit();
                durationDays = faculty.GetBorrowDuration();
            }

            var activeCount = await _loanRepo.GetActiveLoanCountByUser(dto.UserId);
            if (activeCount >= limit) throw new InvalidOperationException($"Borrow limit reached ({limit})");

            var loan = new Loan
            {
                LoanId = Guid.NewGuid().ToString(),
                UserId = dto.UserId,
                ISBN = dto.ISBN,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(durationDays),
                FineAmount = 0m
            };

            await _loanRepo.AddLoan(loan);

            book.AvailableCopies = Math.Max(0, book.AvailableCopies - 1);
            await _bookRepo.UpdateBook(book);

            return new LoanResponseDTO
            {
                LoanId = loan.LoanId,
                UserId = loan.UserId,
                ISBN = loan.ISBN,
                BorrowDate = loan.BorrowDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                FineAmount = loan.FineAmount,
                IsReturned = loan.IsReturned,
                DaysOverdue = loan.DaysOverdue
            };
        }

        public async Task<LoanResponseDTO> GetLoanByIdAsync(string loanId)
        {
            if (string.IsNullOrWhiteSpace(loanId)) return null;
            var l = await _loanRepo.GetLoanById(loanId);
            if (l == null) return null;

            return new LoanResponseDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                ISBN = l.ISBN,
                BorrowDate = l.BorrowDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                FineAmount = l.FineAmount,
                IsReturned = l.IsReturned,
                DaysOverdue = l.DaysOverdue
            };
        }

        public async Task<IEnumerable<LoanResponseDTO>> GetAllLoansAsync()
        {
            var loans = await _loanRepo.GetAllActiveLoans();
            return loans.Select(l => new LoanResponseDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                ISBN = l.ISBN,
                BorrowDate = l.BorrowDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                FineAmount = l.FineAmount,
                IsReturned = l.IsReturned,
                DaysOverdue = l.DaysOverdue
            });
        }

        // FIX FOR CS0535 - MISSING METHOD 1
        public async Task<IEnumerable<LoanResponseDTO>> GetLoansByUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Enumerable.Empty<LoanResponseDTO>();

            var loans = await _loanRepo.GetLoansByUserId(userId);
            return loans.Select(l => new LoanResponseDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                ISBN = l.ISBN,
                BorrowDate = l.BorrowDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                FineAmount = l.FineAmount,
                IsReturned = l.IsReturned,
                DaysOverdue = l.DaysOverdue
            });
        }

        // FIX FOR CS0535 - MISSING METHOD 2
        public async Task<IEnumerable<LoanResponseDTO>> GetOverdueLoansAsync()
        {
            var loans = await _loanRepo.GetAllActiveLoans();
            var overdueLoans = loans.Where(l => l.DaysOverdue > 0);

            return overdueLoans.Select(l => new LoanResponseDTO
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                ISBN = l.ISBN,
                BorrowDate = l.BorrowDate,
                DueDate = l.DueDate,
                ReturnDate = l.ReturnDate,
                FineAmount = l.FineAmount,
                IsReturned = l.IsReturned,
                DaysOverdue = l.DaysOverdue
            });
        }

        public async Task<LoanResponseDTO> ReturnBookAsync(ReturnBookDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.LoanId))
                throw new ArgumentException("LoanId required");

            var loan = await _loanRepo.GetLoanById(dto.LoanId);
            if (loan == null) throw new InvalidOperationException("Loan not found");
            if (loan.ReturnDate != null) throw new InvalidOperationException("Already returned");

            loan.ReturnDate = dto.ActualReturnDate ?? DateTime.UtcNow;

            // FIX FOR CS0815 - PROPERLY AWAIT THE TASK
            if (loan.ReturnDate > loan.DueDate)
            {
                // PROPERLY AWAIT THIS
                var amount = await _fineService.CalculateFine(loan.DueDate, loan.ReturnDate.Value);

                var fineDto = new CreateFineDTO
                {
                    LoanId = loan.LoanId,
                    UserId = loan.UserId,
                    Amount = amount,
                    DueDate = loan.DueDate,
                    Reason = "Overdue return"
                };

                var fine = await _fineService.CreateFineAsync(fineDto);
                loan.FineAmount = fine.Amount;
            }

            await _loanRepo.UpdateLoan(loan);

            var book = await _bookRepo.GetBookByISBN(loan.ISBN);
            if (book != null)
            {
                book.AvailableCopies = Math.Min(book.TotalCopies, book.AvailableCopies + 1);
                await _bookRepo.UpdateBook(book);
            }

            return new LoanResponseDTO
            {
                LoanId = loan.LoanId,
                UserId = loan.UserId,
                ISBN = loan.ISBN,
                BorrowDate = loan.BorrowDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                FineAmount = loan.FineAmount,
                IsReturned = loan.IsReturned,
                DaysOverdue = loan.DaysOverdue
            };
        }
    }
}