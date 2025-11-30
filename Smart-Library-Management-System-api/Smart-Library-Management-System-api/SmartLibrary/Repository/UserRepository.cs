using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextLibrary _ctx;
        public UserRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddUser(User user)
        {
        
            switch (user)
            {
                case Student s:
                    await _ctx.Students.AddAsync(s);
                    break;
                case Faculty f:
                    await _ctx.Faculties.AddAsync(f);
                    break;
                default:
                   
                    throw new InvalidOperationException("Cannot add abstract User. Use Student or Faculty.");
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            switch (user)
            {
                case Student s:
                    _ctx.Students.Update(s);
                    break;
                case Faculty f:
                    _ctx.Faculties.Update(f);
                    break;
            }
            await _ctx.SaveChangesAsync();
        }

     
        public async Task<User> GetUserById(string userId)
        {
            // Try to find in Students first
            var student = await _ctx.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student != null) return student;

            // Try to find in Faculty
            var faculty = await _ctx.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);
            return faculty;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var students = await _ctx.Students.AsNoTracking().ToListAsync();
            var faculties = await _ctx.Faculties.AsNoTracking().ToListAsync();

            var allUsers = new List<User>();
            allUsers.AddRange(students);
            allUsers.AddRange(faculties);
            return allUsers;
        }

        public async Task DeleteUser(string userId)
        {
            var user = await GetUserById(userId);
            if (user == null) return;

            switch (user)
            {
                case Student s:
                    _ctx.Students.Remove(s);
                    break;
                case Faculty f:
                    _ctx.Faculties.Remove(f);
                    break;
            }
            await _ctx.SaveChangesAsync();
        }

        // Faculty-specific methods
        public async Task AddFaculty(Faculty faculty)
        {
           
            await _ctx.Faculties.AddAsync(faculty);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Faculty> GetFacultyById(string userId) =>
            await _ctx.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);

        public async Task<List<Faculty>> GetAllFaculty() =>
            await _ctx.Faculties.AsNoTracking().ToListAsync();

        public async Task UpdateFaculty(Faculty existing)
        {
            _ctx.Faculties.Update(existing);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteFaculty(string userId)
        {
            var f = await GetFacultyById(userId);
            if (f == null) return;
            _ctx.Faculties.Remove(f);
            await _ctx.SaveChangesAsync();
        }

        // Student-specific methods
        public async Task AddStudent(Student student)
        {
           
            await _ctx.Students.AddAsync(student);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(string userId) =>
            await _ctx.Students.FirstOrDefaultAsync(s => s.UserId == userId);

        public async Task<List<Student>> GetAllStudents() =>
            await _ctx.Students.AsNoTracking().ToListAsync();

        public async Task UpdateStudent(Student existing)
        {
            _ctx.Students.Update(existing);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteStudent(string userId)
        {
            var s = await GetStudentById(userId);
            if (s == null) return;
            _ctx.Students.Remove(s);
            await _ctx.SaveChangesAsync();
        }
    }
}