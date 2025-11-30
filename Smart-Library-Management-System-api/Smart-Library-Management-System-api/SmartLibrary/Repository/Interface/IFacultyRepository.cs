using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IFacultyRepository
    {
        Task<Faculty> GetFacultyById(string userId);
        Task<List<Faculty>> GetAllFaculty();
        Task AddFaculty(Faculty faculty);
        Task UpdateFaculty(Faculty faculty);
        Task DeleteFaculty(string userId);
        Task<int> GetActiveLoanCount(string userId);
        
    }
}
