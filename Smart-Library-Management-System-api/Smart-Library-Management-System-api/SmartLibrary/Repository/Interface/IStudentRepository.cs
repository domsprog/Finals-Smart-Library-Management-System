using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IStudentRepository
    {
        Task AddStudent(Student student);
        Task UpdateStudent(Student student);
        Task<Student> GetStudentById(string userId);
        Task<List<Student>> GetAllStudents();
        Task DeleteStudent(string userId);
        Task<int> GetActiveLoanCount(string userId);
    }
}
