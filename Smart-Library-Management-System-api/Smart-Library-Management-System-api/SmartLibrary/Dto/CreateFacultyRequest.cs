namespace Smart_Library_Management_System_api.SmartLibrary.Dto
{
    public class CreateFacultyRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FacultyId { get; set; }      // your Faculty.FacultyId
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
