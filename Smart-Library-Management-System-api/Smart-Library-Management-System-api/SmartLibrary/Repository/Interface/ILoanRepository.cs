using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface ILoanRepository
    {
        Task AddLoan(Loan loan);
        Task UpdateLoan(Loan loan);
        Task<int> GetActiveLoanCountByUser(string userId);
        Task<List<Loan>> GetAllActiveLoans();
        Task<Loan> GetLoanById(string loanId);
        Task<List<Loan>> GetLoansByUserId(string userId);
       
    }
}
