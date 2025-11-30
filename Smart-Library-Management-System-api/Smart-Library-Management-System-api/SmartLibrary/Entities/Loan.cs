using System.ComponentModel.DataAnnotations;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Loan
    {
        [Key]
        public string LoanId { get; set; }
        public string UserId { get; set; }
        public string ISBN { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }

        public bool IsReturned => ReturnDate != null;

        public int DaysOverdue
        {
            get
            {
                var compareDate = ReturnDate ?? DateTime.Now;
                int days = (compareDate - DueDate).Days;
                return days > 0 ? days : 0;
            }
        }
    }
}