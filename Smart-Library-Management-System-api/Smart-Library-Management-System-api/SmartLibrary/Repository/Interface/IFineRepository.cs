using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IFineRepository
    {
        // Get fine by ID
        Task<Fine> GetFineById(string fineId);

        // Get all fines for a user
        Task<List<Fine>> GetFinesByUserId(string userId);

        // Get unpaid fines for a user
        Task<List<Fine>> GetUnpaidFines(string userId);

        // Add a new fine
        Task AddFine(Fine fine);

        // Update existing fine
        Task UpdateFine(Fine fine);

        // Mark fine as paid
        Task MarkFineAsPaid(string fineId);

        // Calculate total unpaid amount for a user
        Task<decimal> GetTotalUnpaidAmount(string userId);
    }

}
