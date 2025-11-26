using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Interface
{
    public interface IBookService
    {
        Task<Book> CreateBook(CreateBookRequest request);
        Task<Book> UpdateBook(string isbn, UpdateBookRequest request);
        Task<bool> DeleteBook(string isbn);
        Task<Book> GetBookByISBN(string isbn);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> SearchBooks(string keyword);
    }
}
