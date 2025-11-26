namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Student : User
    {
        public string StudentId { get; set; }
        public string Department { get; set; }

        public Student(
            string userId,
            string name,
            string email,
            string studentId,
            string department
        ) : base(userId, name, email)
        {
            StudentId = studentId;
            Department = department;
        }

        public override int GetBorrowLimit() => 5;
        public override int GetBorrowCount() => 0;
        public override int GetBorrowDuration() => 14;
    }
}
