using Microsoft.AspNetCore.Mvc;
using SmartLibrary.DTOs.LoanDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDTO dto)
        {
            try
            {
                var result = await _loanService.BorrowBookAsync(dto);
                return CreatedAtAction(nameof(GetLoan), new { loanId = result.LoanId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoan(string loanId)
        {
            var result = await _loanService.GetLoanByIdAsync(loanId);
            if (result == null) return NotFound("Loan not found");
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var result = await _loanService.GetAllLoansAsync();
            return Ok(result);
        }

        // ADDED: Get loans by user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLoansByUser(string userId)
        {
            var loans = await _loanService.GetLoansByUserAsync(userId);
            return Ok(loans);
        }

        // ADDED: Get overdue loans
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueLoans()
        {
            var loans = await _loanService.GetOverdueLoansAsync();
            return Ok(loans);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDTO dto)
        {
            try
            {
                var result = await _loanService.ReturnBookAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}