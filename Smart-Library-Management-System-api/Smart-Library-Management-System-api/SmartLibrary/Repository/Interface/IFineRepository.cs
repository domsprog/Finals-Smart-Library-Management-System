using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IFineRepository
    {
        Task<Fine> GetFineById(string fineId);
        Task<List<Fine>> GetFinesByUser(string userId);
        Task<Fine> AddFine(Fine fine);
        Task UpdateFine(Fine fine);

        // CS0539 FIX #1 - Add this method to interface
        Task<Fine> GetFineByLoan(string loanId);

        Task<bool> PayFine(string fineId);

        // CS0539 FIX #2 - Add this method to interface  
        Task<decimal> CalculateFine(Fine fine);

        // Also add this method that's used by FineService
        Task<List<Fine>> GetAllFines();
    }
}