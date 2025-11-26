using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Dto;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(CreateReservationRequest request);
        Task<bool> CancelReservation(int reservationId);
        Task<List<Reservation>> GetUserReservations(int userId);
    }
}