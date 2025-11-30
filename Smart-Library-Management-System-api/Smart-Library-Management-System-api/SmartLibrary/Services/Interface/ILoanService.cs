using SmartLibrary.DTOs.LoanDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface ILoanService
    {
        Task<LoanResponseDTO> BorrowBookAsync(BorrowBookDTO dto);
        Task<LoanResponseDTO> ReturnBookAsync(ReturnBookDTO dto);
        Task<LoanResponseDTO> GetLoanByIdAsync(string loanId);
        Task<IEnumerable<LoanResponseDTO>> GetAllLoansAsync();
        Task<IEnumerable<LoanResponseDTO>> GetLoansByUserAsync(string userId);
        Task<IEnumerable<LoanResponseDTO>> GetOverdueLoansAsync();
    }
}
