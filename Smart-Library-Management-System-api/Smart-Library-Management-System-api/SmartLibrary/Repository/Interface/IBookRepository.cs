using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task<Book> GetBookByISBN(string isbn);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> SearchBooks(string term);
        Task UpdateBook(Book book);
        Task DeleteBook(string isbn);
        Task<bool> IsBookAvailable(string isbn);
    }
}