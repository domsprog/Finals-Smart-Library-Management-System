using System.ComponentModel.DataAnnotations;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Faculty : User
    {
        public string FacultyId { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        public Faculty(
            string userId,
            string name,
            string email,
            string facultyId,
            string department,
            string position
        ) : base(userId, name, email)
        {
            FacultyId = facultyId;
            Department = department;
            Position = position;
        }

        // ADDED: Parameterless constructor for EF Core
        protected Faculty() : base() { }

        public override int GetBorrowLimit() => 10;
        public override int GetBorrowCount() => 0;
        public override int GetBorrowDuration() => 30;
    }
}