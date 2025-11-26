using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository
{
    public class FineRepository : IFineRepository
    {
        private readonly DbContextLibrary _context;
        //Constructor Injection of the DbContext
        public FineRepository(DbContextLibrary context)
        {
            _context = context;
        }
        // Implement methods defined in IFineRepository interface here
       
        // Get fine by ID
        public async Task<Fine> GetFineById(string fineId)
          {
                return await _context.Fines.FindAsync(fineId);
        }
        // Add a new fine
        public async Task<List<Fine>> GetFinesByUserId(string userId)
        {
            return await _context.Fines
                .Where(fine => fine.UserId == userId)
                .ToListAsync();
        }
        // get unpaid fines by user ID
        public async Task<List<Fine>> GetUnpaidFines(string userId)
        {
            return await _context.Fines
                .Where(fine => fine.UserId == userId && !fine.IsPaid)
                .ToListAsync();
        }
        // Mark fine as paid
        public async Task MarkFineAsPaid(string fineId)
        {
            var fine = await GetFineById(fineId);
            if (fine != null)
            {
                fine.IsPaid = true;
                _context.Fines.Update(fine);
                await _context.SaveChangesAsync();
            }
        }

        //update fine
        public async Task UpdateFine(Fine fine)
        {
            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();
        }
        // Add a new fine
        public async Task AddFine(Fine fine)
        {
            await _context.Fines.AddAsync(fine);
            await _context.SaveChangesAsync();
        }
        // Calculate total unpaid amount for a user
        public async Task<decimal> GetTotalUnpaidAmount(string userId)
        {
            return await _context.Fines
                .Where(fine => fine.UserId == userId && !fine.IsPaid)
                .SumAsync(fine => fine.Amount);
        }


    }
}
