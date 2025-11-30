using Microsoft.AspNetCore.Mvc;
using SmartLibrary.DTOs.FineDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly IFineService _fineService;

        public FineController(IFineService fineService)
        {
            _fineService = fineService;
        }

        // FIXED: Changed to accept CreateFineDTO instead of Fine entity
        [HttpPost]
        public async Task<IActionResult> AddFine([FromBody] CreateFineDTO dto)
        {
            try
            {
                var result = await _fineService.AddFine(dto);
                return CreatedAtAction(nameof(GetFineByIdAsync), new { fineId = result.FineId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFineAsync([FromBody] CreateFineDTO dto)
        {
            try
            {
                var createdFine = await _fineService.CreateFineAsync(dto);
                return CreatedAtAction(nameof(GetFineByIdAsync), new { fineId = createdFine.FineId }, createdFine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        // ADDED: Get fines by user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFinesByUser(string userId)
        {
            var fines = await _fineService.GetFinesByUserAsync(userId);
            return Ok(fines);
        }

        // ADDED: Get fine by loan
        [HttpGet("loan/{loanId}")]
        public async Task<IActionResult> GetFineByLoan(string loanId)
        {
            var fine = await _fineService.GetFineByLoanIdAsync(loanId);
            if (fine == null)
                return NotFound($"No fine found for loan {loanId}.");
            return Ok(fine);
        }

        // FIXED: Changed to PUT for paying fine
        [HttpPut("pay/{fineId}")]
        public async Task<IActionResult> PayFineAsync(string fineId)
        {
            var success = await _fineService.PayFineAsync(fineId);
            if (!success)
            {
                return NotFound($"Fine with ID {fineId} not found or could not be paid.");
            }
            return Ok("Fine paid successfully.");
        }

        // ADDED: Update fine
        [HttpPut("{fineId}")]
        public async Task<IActionResult> UpdateFine(string fineId, [FromBody] UpdateFineDTO dto)
        {
            try
            {
                var updated = await _fineService.UpdateFineAsync(fineId, dto);
                if (updated == null)
                    return NotFound($"Fine with ID {fineId} not found.");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}