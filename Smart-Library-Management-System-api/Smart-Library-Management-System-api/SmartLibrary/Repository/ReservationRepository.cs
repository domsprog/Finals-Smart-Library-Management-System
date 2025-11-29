using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DbContextLibrary _ctx;
        public ReservationRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddReservation(Reservation r)
        {
            
            await _ctx.Reservations.AddAsync(r);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteReservation(string reservationId)
        {
            var r = await GetReservationById(reservationId);
            if (r == null) return;
            _ctx.Reservations.Remove(r);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Reservation> GetReservationById(string reservationId) =>
            await _ctx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);

        public async Task<Reservation> GetReservationByUserAndISBN(string userId, string isbn) =>
            await _ctx.Reservations.FirstOrDefaultAsync(r =>
                r.UserId == userId &&
                r.ISBN == isbn &&
                r.IsActive);

        public async Task<List<Reservation>> GetReservationsByUserId(string userId) =>
            await _ctx.Reservations.Where(r => r.UserId == userId).ToListAsync();

        public async Task UpdateReservation(Reservation r)
        {
            _ctx.Reservations.Update(r);
            await _ctx.SaveChangesAsync();
        }

        
        public async Task<bool> CancelReservation(string reservationId)
        {
            var reservation = await GetReservationById(reservationId);
            if (reservation == null || !reservation.IsActive)
                return false;

            reservation.IsActive = false;
            _ctx.Reservations.Update(reservation);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}