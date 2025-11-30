namespace SmartLibrary.DTOs.FacultyDTOs
{
    public class CreateFacultyDTO
    {
        public string UserId { get; set; }
        public string FacultyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }

    public class UpdateFacultyDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }

    public class FacultyResponseDTO
    {
        public string UserId { get; set; }
        public string FacultyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
