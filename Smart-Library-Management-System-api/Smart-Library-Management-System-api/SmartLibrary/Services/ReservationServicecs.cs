using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.ReservationDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _resRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;

        public ReservationService(IReservationRepository resRepo, IBookRepository bookRepo, IUserRepository userRepo)
        {
            _resRepo = resRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(CreateReservationDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var book = await _bookRepo.GetBookByISBN(dto.ISBN);
            if (book == null) throw new InvalidOperationException("Book not found");
            if (book.AvailableCopies > 0) throw new InvalidOperationException("Book is available; no reservation needed");

            var user = await _userRepo.GetUserById(dto.UserId);
            if (user == null) throw new InvalidOperationException("User not found");

            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid().ToString(),
                UserId = dto.UserId,
                ISBN = dto.ISBN,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7), // 7 days to claim
                IsActive = true,
                IsFulfilled = false 
            };

            await _resRepo.AddReservation(reservation);

            return new ReservationResponseDTO
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.UserId,
                ISBN = reservation.ISBN,
                ReservationDate = reservation.ReservationDate,
                ExpiryDate = reservation.ExpiryDate, 
                IsActive = reservation.IsActive, 
                IsFulfilled = reservation.IsFulfilled 
            };
        }

        public async Task<bool> CancelReservationAsync(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId)) return false;
            return await _resRepo.CancelReservation(reservationId);
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsByUserAsync(string userId)
        {
            var list = await _resRepo.GetReservationsByUserId(userId);
            return list.Select(r => new ReservationResponseDTO
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ISBN = r.ISBN,
                ReservationDate = r.ReservationDate,
                ExpiryDate = r.ExpiryDate, 
                IsActive = r.IsActive, 
                IsFulfilled = r.IsFulfilled 
            });
        }

       
        public async Task<ReservationResponseDTO> GetReservationByIdAsync(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId)) return null;
            var r = await _resRepo.GetReservationById(reservationId);
            if (r == null) return null;

            return new ReservationResponseDTO
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ISBN = r.ISBN,
                ReservationDate = r.ReservationDate,
                ExpiryDate = r.ExpiryDate,
                IsActive = r.IsActive,
                IsFulfilled = r.IsFulfilled
            };
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetActiveReservationsAsync()
        {
            var list = await _resRepo.GetReservationsByUserId(string.Empty); // Get all
            var active = list.Where(r => r.IsActive && !r.IsFulfilled);

            return active.Select(r => new ReservationResponseDTO
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ISBN = r.ISBN,
                ReservationDate = r.ReservationDate,
                ExpiryDate = r.ExpiryDate,
                IsActive = r.IsActive,
                IsFulfilled = r.IsFulfilled
            });
        }

        public async Task<bool> FulfillReservationAsync(string reservationId)
        {
            if (string.IsNullOrWhiteSpace(reservationId)) return false;

            var reservation = await _resRepo.GetReservationById(reservationId);
            if (reservation == null || !reservation.IsActive) return false;

            reservation.IsFulfilled = true;
            reservation.IsActive = false;
            await _resRepo.UpdateReservation(reservation);
            return true;
        }
    }
}