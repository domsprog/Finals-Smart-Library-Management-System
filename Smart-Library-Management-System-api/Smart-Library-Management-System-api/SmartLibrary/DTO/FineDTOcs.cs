
namespace SmartLibrary.DTOs.FineDTOs
{
    public class CreateFineDTO
    {
        public string LoanId { get; set; }
        public string UserId { get; set; } 
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime DueDate { get; set; } 
    }

    public class UpdateFineDTO
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; } 
    }

    public class FineResponseDTO
    {
        public string FineId { get; set; }
        public string LoanId { get; set; }
        public string UserId { get; set; } 
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? PaidDate { get; set; } 
        public DateTime DueDate { get; set; } 
        public string Reason { get; set; } 
    }

    public class CalculateFineDTO
    {
        public string LoanId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}