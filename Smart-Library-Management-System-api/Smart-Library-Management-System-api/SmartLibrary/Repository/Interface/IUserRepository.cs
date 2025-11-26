using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(string userId);

        // STUDENT
        Task AddStudent(Student s);
        Task<Student> GetStudentById(string userId);
        Task<List<Student>> GetAllStudents();
        Task UpdateStudent(Student s);
        Task DeleteStudent(string userId);

        // FACULTY
        Task AddFaculty(Faculty f);
        Task<Faculty> GetFacultyById(string userId);
        Task<List<Faculty>> GetAllFaculty();
        Task UpdateFaculty(Faculty f);
        Task DeleteFaculty(string userId);
    }
}
