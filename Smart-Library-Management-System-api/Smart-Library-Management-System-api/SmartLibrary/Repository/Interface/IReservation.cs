using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IReservationRepository
    {
        Task AddReservation(Reservation r);
        Task UpdateReservation(Reservation r);
        Task<Reservation> GetReservationById(string reservationId);
        Task<List<Reservation>> GetReservationsByUserId(string userId);

        Task<Reservation> GetReservationByUserAndISBN(string userId, string isbn);

        Task DeleteReservation(string reservationId);
        Task<bool> CancelReservation(string reservationId);
    }
}