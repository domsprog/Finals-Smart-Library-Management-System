using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.StudentDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        public StudentService(IStudentRepository studentRepo) => _studentRepo = studentRepo;

        public async Task<StudentResponseDTO> CreateStudentAsync(CreateStudentDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var userId = string.IsNullOrWhiteSpace(dto.UserId) ? Guid.NewGuid().ToString() : dto.UserId;

            var student = new Student(userId, dto.Name, dto.Email, dto.StudentId, dto.Department);
            await _studentRepo.AddStudent(student);

            return new StudentResponseDTO
            {
                UserId = student.UserId,
                StudentId = student.StudentId,
                Name = student.Name,
                Email = student.Email,
                Department = student.Department,
                RegisteredDate = student.RegisteredDate 
            };
        }

        public async Task<bool> DeleteStudentAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return false;
            var existing = await _studentRepo.GetStudentById(userId);
            if (existing == null) return false;
            await _studentRepo.DeleteStudent(userId);
            return true;
        }

        public async Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var list = await _studentRepo.GetAllStudents();
            return list.Select(s => new StudentResponseDTO
            {
                UserId = s.UserId,
                StudentId = s.StudentId,
                Name = s.Name,
                Email = s.Email,
                Department = s.Department,
                RegisteredDate = s.RegisteredDate 
            });
        }

        public async Task<StudentResponseDTO> GetStudentByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return null;
            var s = await _studentRepo.GetStudentById(userId);
            if (s == null) return null;
            return new StudentResponseDTO
            {
                UserId = s.UserId,
                StudentId = s.StudentId,
                Name = s.Name,
                Email = s.Email,
                Department = s.Department,
                RegisteredDate = s.RegisteredDate 
            };
        }

        public async Task<StudentResponseDTO> UpdateStudentAsync(string userId, UpdateStudentDTO dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("userId required");
            var s = await _studentRepo.GetStudentById(userId);
            if (s == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                s.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email)) 
                s.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Department))
                s.Department = dto.Department;

            await _studentRepo.UpdateStudent(s);

            return new StudentResponseDTO
            {
                UserId = s.UserId,
                StudentId = s.StudentId,
                Name = s.Name,
                Email = s.Email,
                Department = s.Department,
                RegisteredDate = s.RegisteredDate
            };
        }
    }
}