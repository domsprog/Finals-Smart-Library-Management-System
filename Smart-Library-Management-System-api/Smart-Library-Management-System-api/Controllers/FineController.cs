//using Microsoft.AspNetCore.Mvc;
//using Smart_Library_Management_System_api.SmartLibrary.Entities;
//using SmartLibrary.DTOs.FineDTOs;
//using SmartLibrary.Services.FineService;
//using SmartLibrary.Services.Interfaces;

//namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FineController : ControllerBase
//    {


//        [HttpPost()]
//        public async Task<IActionResult> AddFine(Fine fine)
//        {
//            var result = await fineService.AddFine(fine);
//            return Ok(result);
//        }
//        [HttpPost("create")]
//        public async Task<IActionResult> CreateFineAsync(CreateFineDTO dto)
//        {
//            var createdFine = await fineService.CreateFineAsync(dto);
//            return Ok(createdFine);
//        }


//        private readonly IFineService fineService;
//        [HttpGet]
//        public async Task<IActionResult> GetAllFinesAsync()
//        {
//            var fines = await fineService.GetAllFinesAsync();
//            return Ok(fines);
//        }

//        [HttpGet("{fineId}")]
//        public async Task<IActionResult> GetFineByIdAsync(string fineId)
//        {
//            var fine = await fineService.GetFineByIdAsync(fineId);
//            if (fine == null)
//            {
//                return NotFound($"Fine with ID {fineId} not found.");
//            }
//            return Ok(fine);
//        }
//        [HttpDelete("{fineId}")]
//        public async Task<IActionResult> PayFineAsync(string fineId)
//        {
//            var success = await fineService.PayFineAsync(fineId);
//            if (!success)
//            {
//                return NotFound($"Fine with ID {fineId} not found or could not be paid.");
//            }
//            return Ok("Fine paid successfully.");
//        }

//    }
//}
using Microsoft.AspNetCore.Mvc;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using SmartLibrary.DTOs.FineDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        // FIXED: Moved field declaration to the top and made it readonly
        private readonly IFineService _fineService;

        // FIXED: Added constructor
        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFine(Fine fine)
        {
            var result = await _fineService.AddFine(fine);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFineAsync(CreateFineDTO dto)
        {
            var createdFine = await _fineService.CreateFineAsync(dto);
            return Ok(createdFine);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFinesAsync()
        {
            var fines = await _fineService.GetAllFinesAsync();
            return Ok(fines);
        }

        [HttpGet("{fineId}")]
        public async Task<IActionResult> GetFineByIdAsync(string fineId)
        {
            var fine = await _fineService.GetFineByIdAsync(fineId);
            if (fine == null)
            {
                return NotFound($"Fine with ID {fineId} not found.");
            }
            return Ok(fine);
        }

        [HttpDelete("{fineId}")]
        public async Task<IActionResult> PayFineAsync(string fineId)
        {
            var success = await _fineService.PayFineAsync(fineId);
            if (!success)
            {
                return NotFound($"Fine with ID {fineId} not found or could not be paid.");
            }
            return Ok("Fine paid successfully.");
        }
    }
}