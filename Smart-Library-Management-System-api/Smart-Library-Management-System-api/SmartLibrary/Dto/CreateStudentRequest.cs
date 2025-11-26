namespace Smart_Library_Management_System_api.SmartLibrary.Dto
{
    public class CreateStudentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; }      // your Student.StudentId
        public string Department { get; set; }
    }
}
