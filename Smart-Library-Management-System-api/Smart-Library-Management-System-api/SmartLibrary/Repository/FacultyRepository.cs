using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly DbContextLibrary _ctx;
        public FacultyRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddFaculty(Faculty faculty)
        {
            await _ctx.Faculties.AddAsync(faculty);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteFaculty(string userId)
        {
            var f = await GetFacultyById(userId);
            if (f == null) return;
            _ctx.Faculties.Remove(f);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Faculty>> GetAllFaculty() =>
            await _ctx.Faculties.AsNoTracking().ToListAsync();

        public async Task<Faculty> GetFacultyById(string userId) =>
            await _ctx.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);

        public async Task<int> GetActiveLoanCount(string userId) =>
            await _ctx.Loans.CountAsync(l => l.UserId == userId && l.ReturnDate == null);

        public async Task UpdateFaculty(Faculty faculty)
        {
            _ctx.Faculties.Update(faculty);
            await _ctx.SaveChangesAsync();
        }
    }
}
