using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Dto;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(CreateStudentRequest request);
        Task<Student> GetStudentByIdAsync(string userId);
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> UpdateStudentAsync(string userId, UpdateUserRequest request);
        Task<bool> DeleteStudentAsync(string userId);
    }
}
