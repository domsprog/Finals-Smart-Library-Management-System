using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userRepo.GetUserById(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsers();
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var existing = await _userRepo.GetUserById(userId);
            if (existing == null) return false;
            await _userRepo.DeleteUser(userId);
            return true;
        }
    }
}
