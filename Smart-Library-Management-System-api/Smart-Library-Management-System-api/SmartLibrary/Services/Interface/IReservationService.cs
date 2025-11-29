using SmartLibrary.DTOs.ReservationDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponseDTO> CreateReservationAsync(CreateReservationDTO dto);
        Task<ReservationResponseDTO> GetReservationByIdAsync(string reservationId);
        Task<IEnumerable<ReservationResponseDTO>> GetReservationsByUserAsync(string userId);
        Task<IEnumerable<ReservationResponseDTO>> GetActiveReservationsAsync();
        Task<bool> CancelReservationAsync(string reservationId);
        Task<bool> FulfillReservationAsync(string reservationId);
    }
}
