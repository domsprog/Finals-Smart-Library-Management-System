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
            var result = await _loanService.BorrowBookAsync(dto);
            return Ok(result);
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

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDTO dto)
        {
            var result = await _loanService.ReturnBookAsync(dto);
            return Ok(result);
        }
    }
}
