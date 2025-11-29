using SmartLibrary.DTOs.FacultyDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface IFacultyService
    {
        Task<FacultyResponseDTO> CreateFacultyAsync(CreateFacultyDTO dto);
        Task<FacultyResponseDTO> UpdateFacultyAsync(string userId, UpdateFacultyDTO dto);
        Task<bool> DeleteFacultyAsync(string userId);
        Task<FacultyResponseDTO> GetFacultyByIdAsync(string userId);
        Task<IEnumerable<FacultyResponseDTO>> GetAllFacultyAsync();
    }
}
