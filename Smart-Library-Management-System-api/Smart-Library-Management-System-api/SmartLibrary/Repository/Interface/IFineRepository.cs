using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IFineRepository
    {
        Task<Fine> GetFineById(string fineId);
        Task<List<Fine>> GetFinesByUser(string userId);
        Task<Fine> AddFine(Fine fine);
        Task UpdateFine(Fine fine);

       
        Task<Fine> GetFineByLoan(string loanId);

        Task<bool> PayFine(string fineId);

   
        Task<decimal> CalculateFine(Fine fine);

        Task<List<Fine>> GetAllFines();
    }
}