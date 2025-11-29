namespace SmartLibrary.DTOs.UserDTOs
{
    public class CreateUserDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string UserType { get; set; } 
        public string StudentId { get; set; }
        public string FacultyId { get; set; }
        public string Position { get; set; }
    }

    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; } 
    }

    public class UserResponseDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string UserType { get; set; } 
        public string StudentId { get; set; }
        public string FacultyId { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }

    public class DeleteUserDTO
    {
        public string UserId { get; set; }
    }
}