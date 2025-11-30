using SmartLibrary.DTOs.FineDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface IFineService
    {
        // FIXED: Changed from returning raw entity to DTO
        Task<FineResponseDTO> AddFine(CreateFineDTO dto);

        Task<FineResponseDTO> CreateFineAsync(CreateFineDTO dto);
        Task<FineResponseDTO> UpdateFineAsync(string fineId, UpdateFineDTO dto);
        Task<bool> PayFineAsync(string fineId);
        Task<FineResponseDTO> GetFineByIdAsync(string fineId);
        Task<IEnumerable<FineResponseDTO>> GetAllFinesAsync();
        Task<IEnumerable<FineResponseDTO>> GetFinesByUserAsync(string userId);
        Task<decimal> CalculateFine(DateTime dueDate, DateTime returnDate);
        Task<FineResponseDTO> GetFineByLoanIdAsync(string loanId);
    }
}