namespace SmartLibrary.DTOs.StudentDTOs
{
    public class CreateStudentDTO
    {
        public string UserId { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
    }

    public class UpdateStudentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Department { get; set; }
    }

    public class StudentResponseDTO
    {
        public string UserId { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime RegisteredDate { get; set; } 
    }
}
