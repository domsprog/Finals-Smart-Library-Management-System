using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface ILoanService
    {
        Task<Loan> BorrowBook(BorrowBookRequest request);
        Task<Loan> ReturnBook(int loanId);
        Task<List<Loan>> GetUserLoans(int userId);
    }
}