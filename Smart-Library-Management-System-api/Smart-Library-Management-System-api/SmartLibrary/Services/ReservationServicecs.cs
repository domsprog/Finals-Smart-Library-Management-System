using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IReservationRepository _reservationRepo;
        private readonly IUserRepository _userRepo;

        public ReservationService(IBookRepository bookRepo,
            IReservationRepository reservationRepo,
            IUserRepository userRepo)
        {
            _bookRepo = bookRepo;
            _reservationRepo = reservationRepo;
            _userRepo = userRepo;
        }

        public async Task<Reservation> CreateReservation(CreateReservationRequest request)
        {
            var book = await _bookRepo.GetBookByISBN(request.ISBN);
            if (book == null) throw new InvalidOperationException("Book not found.");

            if (book.IsAvailable)
                throw new InvalidOperationException("Book is available — no need to reserve.");

            var user = await _userRepo.GetUserById(request.UserId);
            if (user == null) throw new InvalidOperationException("User not found.");

            // optional: check existing active reservation by the same user for the same book
            var existing = await _reservationRepo.GetReservationByUserAndISBN(request.UserId, request.ISBN);
            if (existing != null) throw new InvalidOperationException("You already have a reservation for this book.");

            var reservation = new Reservation
            {
                UserId = request.UserId,
                BookId = request.ISBN,
                ReservedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _reservationRepo.AddReservation(reservation);
            return reservation;
        }

        public async Task<bool> CancelReservation(int reservationId)
        {
            var reservation = await _reservationRepo.GetReservationById(reservationId);
            if (reservation == null) return false;

            reservation.IsActive = false;
            await _reservationRepo.UpdateReservation(reservation);
            return true;
        }

        public async Task<List<Reservation>> GetUserReservations(int userId)
        {
            return await _reservationRepo.GetReservationsByUserId(userId);
        }
    }
}
