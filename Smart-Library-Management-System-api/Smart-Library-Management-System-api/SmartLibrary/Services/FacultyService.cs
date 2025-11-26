using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class FacultyService : IFacultyService
    {
        private readonly IUserRepository _userRepo;
        public FacultyService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Faculty> CreateFacultyAsync(CreateFacultyRequest request)
        {
            var userId = Guid.NewGuid().ToString();

            var faculty = new Faculty(userId, request.Name, request.Email, request.FacultyId, request.Department, request.Position);

            await _userRepo.AddFaculty(faculty);
            return faculty;
        }

        public async Task<Faculty> GetFacultyByIdAsync(string userId)
        {
            return await _userRepo.GetFacultyById(userId);
        }

        public async Task<List<Faculty>> GetAllFacultyAsync()
        {
            return await _userRepo.GetAllFaculty();
        }

        public async Task<Faculty> UpdateFacultyAsync(string userId, UpdateUserRequest request)
        {
            var existing = await _userRepo.GetFacultyById(userId);
            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Name)) existing.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Email)) existing.Email = request.Email;

            await _userRepo.UpdateFaculty(existing);
            return existing;
        }

        public async Task<bool> DeleteFacultyAsync(string userId)
        {
            var existing = await _userRepo.GetFacultyById(userId);
            if (existing == null) return false;
            await _userRepo.DeleteFaculty(userId);
            return true;
        }
    }
}
