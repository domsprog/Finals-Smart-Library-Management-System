using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DbContextLibrary _ctx;
        public LoanRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddLoan(Loan loan)
        {
            await _ctx.Loans.AddAsync(loan);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Loan> GetLoanById(string loanId) =>
            await _ctx.Loans.FirstOrDefaultAsync(l => l.LoanId == loanId);

        public async Task<List<Loan>> GetLoansByUserId(string userId) =>
            await _ctx.Loans.Where(l => l.UserId == userId).ToListAsync();

        public async Task<int> GetActiveLoanCountByUser(string userId) =>
            await _ctx.Loans.CountAsync(l => l.UserId == userId && l.ReturnDate == null);

        public async Task<List<Loan>> GetAllActiveLoans() =>
            await _ctx.Loans.Where(l => l.ReturnDate == null).ToListAsync();

        public async Task UpdateLoan(Loan loan)
        {
            _ctx.Loans.Update(loan);
            await _ctx.SaveChangesAsync();
        }
    }
}
