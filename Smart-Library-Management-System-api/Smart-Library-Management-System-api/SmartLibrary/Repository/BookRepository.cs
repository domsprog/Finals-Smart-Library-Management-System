using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DbContextLibrary _context;

        //Constructor Injection of the DbContext
        public BookRepository(DbContextLibrary context)
        {
            _context = context;
        }
        // Get a single book by ISBN
        public async Task<Book> GetBookByISBN(string isbn)
        {
            return await _context.Books.FindAsync(isbn);
        }
        // Get all books
        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // Search books by title or author
        public async Task<List<Book>> SearchBooks(string searchTerm)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm))
                .ToListAsync();
        }

        // Add a new book
        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        // Update existing book
        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        // Delete a book
        public async Task DeleteBook(string isbn)
        {
            var book = await GetBookByISBN(isbn);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        // Check if book is available
        public async Task<bool> IsBookAvailable(string isbn)
        {
            var book = await GetBookByISBN(isbn);
            return book != null; // Simplified availability check
        }
        // Get books by author
        public async Task<List<Book>> GetBooksByAuthor(string author)
        {
            return await _context.Books
                .Where(b => b.Author == author)
                .ToListAsync();
        }
    }
}
