using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IFineService
    {
        Task<Fine> CreateFine(int loanId, decimal amount);
        Task<List<Fine>> GetUserFines(int userId);
        Task<decimal> CalculateFine(DateTime dueDate, DateTime returnedDate);
    }
}
