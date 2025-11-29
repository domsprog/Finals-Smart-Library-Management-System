namespace SmartLibrary.DTOs.ReservationDTOs
{
    public class CreateReservationDTO
    {
        public string UserId { get; set; }
        public string ISBN { get; set; }
    }

    public class ReservationResponseDTO
    {
        public string ReservationId { get; set; }
        public string UserId { get; set; }
        public string ISBN { get; set; }
        public DateTime ReservationDate { get; set; } 
        public DateTime ExpiryDate { get; set; } 
        public bool IsActive { get; set; } 
        public bool IsFulfilled { get; set; } 
    }
}