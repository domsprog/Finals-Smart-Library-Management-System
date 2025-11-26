using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextLibrary _context;
        public UserRepository(DbContextLibrary context)
        {
            _context = context;
        }

        // ================== USER ==================
        public async Task<User> GetUserById(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await GetUserById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // ================== STUDENT ==================
        public async Task AddStudent(Student s)
        {
            await _context.Students.AddAsync(s);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(string userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task UpdateStudent(Student s)
        {
            _context.Students.Update(s);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(string userId)
        {
            var student = await GetStudentById(userId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        // ================== FACULTY ==================
        public async Task AddFaculty(Faculty f)
        {
            await _context.Faculties.AddAsync(f);
            await _context.SaveChangesAsync();
        }

        public async Task<Faculty> GetFacultyById(string userId)
        {
            return await _context.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task<List<Faculty>> GetAllFaculty()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task UpdateFaculty(Faculty f)
        {
            _context.Faculties.Update(f);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFaculty(string userId)
        {
            var faculty = await GetFacultyById(userId);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
        }
    }
}
