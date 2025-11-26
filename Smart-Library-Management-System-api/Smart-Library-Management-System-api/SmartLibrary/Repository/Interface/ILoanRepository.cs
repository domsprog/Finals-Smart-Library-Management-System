using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface ILoanRepository
    {
        // Get loan by ID
        Task<Loan> GetLoanById(string loanId);

        // Get all loans for a user
        Task<List<Loan>> GetLoansByUserId(string userId);

        // Get active (not returned) loans for a user
        Task<List<Loan>> GetActiveLoans(string userId);

        // Get all overdue loans
        Task<List<Loan>> GetOverdueLoans();

        // Add a new loan
        Task AddLoan(Loan loan);

        // Update existing loan
        Task UpdateLoan(Loan loan);

        // Count active loans for a user
        Task<int> CountActiveLoans(string userId);
        Task<Loan> GetLoanById(int loanId);
        Task<List<Loan>> GetLoansByUserId(int userId);
    }
}
