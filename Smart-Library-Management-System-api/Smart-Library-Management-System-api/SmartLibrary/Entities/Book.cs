using System.ComponentModel.DataAnnotations;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Book
    {
        private string _isbn;
        private string _title;
        private decimal _price;

        [Key]
        public string ISBN
        {
            get => _isbn;
            set => _isbn = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("ISBN required");
        }

        public string Title
        {
            get => _title;
            set => _title = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Title required");
        }

        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Category { get; set; }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentException("Price must be non-negative");
        }

        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public bool IsAvailable => AvailableCopies > 0;
    }
}