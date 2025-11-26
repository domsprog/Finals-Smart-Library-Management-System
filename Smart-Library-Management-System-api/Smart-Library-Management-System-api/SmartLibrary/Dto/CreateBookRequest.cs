namespace Smart_Library_Management_System_api.SmartLibrary.Dto
{
    public class CreateBookRequest
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }

}
