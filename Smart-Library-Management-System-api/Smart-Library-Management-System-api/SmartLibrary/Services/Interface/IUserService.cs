using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string userId);
        
    }
}
