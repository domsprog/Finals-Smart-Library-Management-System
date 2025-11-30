using Microsoft.AspNetCore.Mvc;
using SmartLibrary.DTOs.ReservationDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO dto)
        {
            try
            {
                var reservation = await _service.CreateReservationAsync(dto);
                return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservation.ReservationId }, reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationById(string reservationId)
        {
            var reservation = await _service.GetReservationByIdAsync(reservationId);
            if (reservation == null)
                return NotFound($"Reservation {reservationId} not found.");
            return Ok(reservation);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(string userId)
        {
            var reservations = await _service.GetReservationsByUserAsync(userId);
            return Ok(reservations);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveReservations()
        {
            var reservations = await _service.GetActiveReservationsAsync();
            return Ok(reservations);
        }

        [HttpPut("cancel/{reservationId}")]
        public async Task<IActionResult> CancelReservation(string reservationId)
        {
            var result = await _service.CancelReservationAsync(reservationId);
            if (!result)
                return NotFound($"Reservation {reservationId} not found or already cancelled.");
            return Ok("Reservation cancelled successfully");
        }

        [HttpPut("fulfill/{reservationId}")]
        public async Task<IActionResult> FulfillReservation(string reservationId)
        {
            var result = await _service.FulfillReservationAsync(reservationId);
            if (!result)
                return NotFound($"Reservation {reservationId} not found or already fulfilled.");
            return Ok("Reservation fulfilled successfully");
        }
    }
}