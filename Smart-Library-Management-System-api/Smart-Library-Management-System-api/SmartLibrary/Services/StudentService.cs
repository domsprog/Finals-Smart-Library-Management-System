using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUserRepository _userRepo;
        public StudentService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Student> CreateStudentAsync(CreateStudentRequest request)
        {
            // generate UserId
            var userId = Guid.NewGuid().ToString();

            var student = new Student(userId, request.Name, request.Email, request.StudentId, request.Department);

            // repository expected to add student (should persist type Student)
            await _userRepo.AddStudent(student);
            return student;
        }

        public async Task<Student> GetStudentByIdAsync(string userId)
        {
            return await _userRepo.GetStudentById(userId);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _userRepo.GetAllStudents();
        }

        public async Task<Student> UpdateStudentAsync(string userId, UpdateUserRequest request)
        {
            var existing = await _userRepo.GetStudentById(userId);
            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Name)) existing.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Email)) existing.Email = request.Email;

            await _userRepo.UpdateStudent(existing);
            return existing;
        }

        public async Task<bool> DeleteStudentAsync(string userId)
        {
            var existing = await _userRepo.GetStudentById(userId);
            if (existing == null) return false;
            await _userRepo.DeleteStudent(userId);
            return true;
        }
    }
}
