using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DbContextLibrary _context;
        //Constructor Injection of the DbContext
        public LoanRepository(DbContextLibrary context)
        {
            _context = context;
        }
        // Implement methods defined in ILoanRepository interface here
        // For example:
       public async Task<Loan> GetLoanById(string loanId)
        {
            return await _context.Loans.FindAsync(loanId);
        }
        public async Task<List<Loan>> GetLoansByUserId(string userId)
        {
            return await _context.Loans
                .Where(loan => loan.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Loan>> GetActiveLoans(string userId)
        {
            return await _context.Loans
                .Where(loan => loan.UserId == userId && loan.ReturnDate == null)
                .ToListAsync();
        }
        public async Task<List<Loan>> GetOverdueLoans()
        {
            var today = DateTime.UtcNow;
            return await _context.Loans
                .Where(loan => loan.DueDate < today && loan.ReturnDate == null)
                .ToListAsync();
        }
        public async Task AddLoan(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLoan(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountActiveLoans(string userId)
        {
            return await _context.Loans
                .CountAsync(loan => loan.UserId == userId && loan.ReturnDate == null);
        }

        public Task<Loan> GetLoanById(int loanId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Loan>> GetLoansByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
