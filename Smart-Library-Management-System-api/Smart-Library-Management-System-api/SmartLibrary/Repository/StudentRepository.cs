using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbContextLibrary _ctx;
        public StudentRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddStudent(Student student)
        {
            await _ctx.Students.AddAsync(student);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteStudent(string userId)
        {
            var s = await GetStudentById(userId);
            if (s == null) return;
            _ctx.Students.Remove(s);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudents() =>
            await _ctx.Students.AsNoTracking().ToListAsync();

        public async Task<Student> GetStudentById(string userId) =>
            await _ctx.Students.FirstOrDefaultAsync(s => s.UserId == userId);

        public async Task<int> GetActiveLoanCount(string userId) =>
            await _ctx.Loans.CountAsync(l => l.UserId == userId && l.ReturnDate == null);

        public async Task UpdateStudent(Student student)
        {
            _ctx.Students.Update(student);
            await _ctx.SaveChangesAsync();
        }
    }
}
