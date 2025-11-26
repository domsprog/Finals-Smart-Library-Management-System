using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Dto;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IFacultyService
    {
        Task<Faculty> CreateFacultyAsync(CreateFacultyRequest request);
        Task<Faculty> GetFacultyByIdAsync(string userId);
        Task<List<Faculty>> GetAllFacultyAsync();
        Task<Faculty> UpdateFacultyAsync(string userId, UpdateUserRequest request);
        Task<bool> DeleteFacultyAsync(string userId);
    }
}
