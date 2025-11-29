using SmartLibrary.DTOs.FacultyDTOs;
using SmartLibrary.DTOs.StudentDTOs;
using SmartLibrary.DTOs.UserDTOs;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IUserService
    {
    
        Task<UserResponseDTO> RegisterStudent(CreateStudentDTO dto);
        Task<UserResponseDTO> RegisterFaculty(CreateFacultyDTO dto);

   
        Task<UserResponseDTO> GetUserById(string userId);
        Task<IEnumerable<UserResponseDTO>> GetAllUsers();

      
        Task<UserResponseDTO> UpdateUser(string userId, UpdateUserDTO dto);

        Task<bool> DeleteUser(string userId);
    }
}