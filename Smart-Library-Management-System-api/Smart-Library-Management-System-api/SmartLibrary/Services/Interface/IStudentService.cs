using SmartLibrary.DTOs.StudentDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentResponseDTO> CreateStudentAsync(CreateStudentDTO dto);
        Task<StudentResponseDTO> UpdateStudentAsync(string userId, UpdateStudentDTO dto);
        Task<bool> DeleteStudentAsync(string userId);
        Task<StudentResponseDTO> GetStudentByIdAsync(string userId);
        Task<IEnumerable<StudentResponseDTO>> GetAllStudentsAsync();
    }
}
