using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;
using SmartLibrary.DTOs.FacultyDTOs;
using SmartLibrary.DTOs.StudentDTOs;
using SmartLibrary.DTOs.UserDTOs;

namespace Smart_Library_Management_System_api.SmartLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserResponseDTO> RegisterStudent(CreateStudentDTO dto)
        {
            var student = new Student(
                dto.UserId ?? Guid.NewGuid().ToString(),
                dto.Name,
                dto.Email,
                dto.StudentId,
                dto.Department
            );

            await _userRepo.AddUser(student);

            return new UserResponseDTO
            {
                UserId = student.UserId,
                Name = student.Name,
                Email = student.Email,
                RegisteredDate = student.RegisteredDate,
                UserType = "Student",
                StudentId = student.StudentId,
                Department = student.Department
            };
        }

        public async Task<UserResponseDTO> RegisterFaculty(CreateFacultyDTO dto)
        {
            var faculty = new Faculty(
                dto.UserId ?? Guid.NewGuid().ToString(),
                dto.Name,
                dto.Email,
                dto.FacultyId,
                dto.Department,
                dto.Position
            );

            await _userRepo.AddUser(faculty);

            return new UserResponseDTO
            {
                UserId = faculty.UserId,
                Name = faculty.Name,
                Email = faculty.Email,
                RegisteredDate = faculty.RegisteredDate,
                UserType = "Faculty",
                FacultyId = faculty.FacultyId,
                Position = faculty.Position,
                Department = faculty.Department
            };
        }

        public async Task<UserResponseDTO> GetUserById(string id)
        {
            var user = await _userRepo.GetUserById(id);
            if (user == null) return null;

            return MapToDTO(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();
            return users.Select(MapToDTO);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userRepo.GetUserById(userId);
            if (user == null) return false;

            await _userRepo.DeleteUser(userId);
            return true;
        }

        public async Task<UserResponseDTO> UpdateUser(string userId, UpdateUserDTO dto)
        {
            var user = await _userRepo.GetUserById(userId);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            switch (user)
            {
                case Student s:
                    if (!string.IsNullOrWhiteSpace(dto.Department))
                        s.Department = dto.Department;
                    break;

                case Faculty f:
                    if (!string.IsNullOrWhiteSpace(dto.Department))
                        f.Department = dto.Department;
                    if (!string.IsNullOrWhiteSpace(dto.Position))
                        f.Position = dto.Position;
                    break;
            }

            await _userRepo.UpdateUser(user);
            return MapToDTO(user);
        }

        private UserResponseDTO MapToDTO(User user)
        {
            return user switch
            {
                Student s => new UserResponseDTO
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Email = s.Email,
                    RegisteredDate = s.RegisteredDate,
                    UserType = "Student",
                    StudentId = s.StudentId,
                    Department = s.Department
                },
                Faculty f => new UserResponseDTO
                {
                    UserId = f.UserId,
                    Name = f.Name,
                    Email = f.Email,
                    RegisteredDate = f.RegisteredDate,
                    UserType = "Faculty",
                    FacultyId = f.FacultyId,
                    Position = f.Position,
                    Department = f.Department
                },
                _ => null
            };
        }
    }
}