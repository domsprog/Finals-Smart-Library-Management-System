namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Fine
    {
        public string FineId { get; set; }
        public string LoanId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Reason { get; set; }
        public DateTime IssuedAt { get; internal set; }
    }
}
