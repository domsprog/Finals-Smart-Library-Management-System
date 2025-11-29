using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class FineRepository : IFineRepository
    {
        private readonly DbContextLibrary _ctx;
        public FineRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task<Fine> AddFine(Fine fine)
        {
            await _ctx.Fines.AddAsync(fine);
            await _ctx.SaveChangesAsync();
            return fine;
        }

        public async Task<Fine> GetFineById(string fineId) =>
            await _ctx.Fines.FirstOrDefaultAsync(f => f.FineId == fineId);

        public async Task<Fine> GetFineByLoan(string loanId) =>
            await _ctx.Fines.FirstOrDefaultAsync(f => f.LoanId == loanId);

        public async Task<List<Fine>> GetFinesByUser(string userId) =>
            await _ctx.Fines.Where(f => f.UserId == userId).ToListAsync();

        public async Task<bool> PayFine(string fineId)
        {
            var fine = await GetFineById(fineId);
            if (fine == null) return false;

            fine.IsPaid = true;
            fine.PaidDate = DateTime.Now;
            _ctx.Fines.Update(fine);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task UpdateFine(Fine fine)
        {
            _ctx.Fines.Update(fine);
            await _ctx.SaveChangesAsync();
        }

        public async Task<decimal> CalculateFine(Fine fine)
        {
            if (fine.DueDate >= DateTime.Now)
                return 0;

            var daysOverdue = (DateTime.Now - fine.DueDate).Days;
            var fineAmount = daysOverdue * 5.0m;

            fine.Amount = fineAmount;
            _ctx.Fines.Update(fine);
            await _ctx.SaveChangesAsync();

            return fineAmount;
        }

        public async Task<List<Fine>> GetAllFines() =>
            await _ctx.Fines.AsNoTracking().ToListAsync();
    }
}