using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface IBookRepository
    {
        // Get a single book by ISBN
        Task<Book> GetBookByISBN(string isbn);

        // Get all books
        Task<List<Book>> GetAllBooks();

        // Search books by title or author
        Task<List<Book>> SearchBooks(string searchTerm);

        // Add a new book
        Task AddBook(Book book);

        // Update existing book
        Task UpdateBook(Book book);

        // Delete a book
        Task DeleteBook(string isbn);

        // Check if book is available
        Task<bool> IsBookAvailable(string isbn);
    }

}
