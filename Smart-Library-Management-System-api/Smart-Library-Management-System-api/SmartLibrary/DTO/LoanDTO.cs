namespace SmartLibrary.DTOs.LoanDTOs
{
    public class BorrowBookDTO
    {
        public string UserId { get; set; }
        public string ISBN { get; set; }
    }

    public class ReturnBookDTO
    {
        public string LoanId { get; set; }
        public DateTime? ActualReturnDate { get; set; } 
    }

    public class LoanResponseDTO
    {
        public string LoanId { get; set; }
        public string UserId { get; set; }
        public string ISBN { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }
        public bool IsReturned { get; set; } 
        public int DaysOverdue { get; set; } 
    }
}