using System.ComponentModel.DataAnnotations;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Reservation
    {
        [Key]
        public string ReservationId { get; set; }
        public string UserId { get; set; }
        public string ISBN { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsFulfilled { get; set; }

    }
}