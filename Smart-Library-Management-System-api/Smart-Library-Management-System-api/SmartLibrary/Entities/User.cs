using System.ComponentModel.DataAnnotations;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public abstract class User
    {
        private string _userId;
        private string _name;
        private string _email;

        [Key]
        public string UserId
        {
            get => _userId;
            set => _userId = value ?? throw new ArgumentNullException(nameof(UserId));
        }

        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Name cannot be empty");
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Invalid email format");
                _email = value;
            }
        }

        public DateTime RegisteredDate { get; set; }

        public abstract int GetBorrowLimit();
        public abstract int GetBorrowCount();
        public abstract int GetBorrowDuration();

        protected User(string userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
            RegisteredDate = DateTime.Now;
        }

      
        protected User()
        {
            RegisteredDate = DateTime.Now;
        }
    }
}