using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task<User> GetUserById(string userId);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(string userId);

        Task AddFaculty(Faculty faculty);
        Task<Faculty> GetFacultyById(string userId);
        Task<List<Faculty>> GetAllFaculty();

        Task UpdateFaculty(Faculty existing);
        Task DeleteFaculty(string userId);

        Task AddStudent(Student student);

        Task UpdateStudent(Student existing);

        Task<Student> GetStudentById(string userId);

        Task<List<Student>> GetAllStudents();
        Task DeleteStudent(string userId);
    }
}