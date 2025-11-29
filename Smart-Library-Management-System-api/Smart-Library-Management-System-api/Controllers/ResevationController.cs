//using Microsoft.AspNetCore.Mvc;
//using SmartLibrary.DTOs.ReservationDTOs;
//using SmartLibrary.Services.Interfaces;
//using SmartLibrary.Services.ReservationService;

//namespace Smart_Library_Management_System_api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ReservationsController : ControllerBase
//    {
//        private readonly IReservationService _service;

//        public ReservationsController(IReservationService service)
//        {
//            _service = service;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Reserve(CreateReservationDTO dto)
//        {
//            return Ok(await _service.CreateReservationAsync(dto));
//        }
//        [HttpPost]

//    }
//}
