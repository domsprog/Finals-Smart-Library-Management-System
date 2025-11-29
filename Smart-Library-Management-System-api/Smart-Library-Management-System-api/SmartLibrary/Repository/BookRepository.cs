using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly DbContextLibrary _ctx;

        public BookRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            await _ctx.Books.AddAsync(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN is required", nameof(isbn));

            var book = await GetBookByISBN(isbn);
            if (book == null) return;

            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooks() =>
            await _ctx.Books.AsNoTracking().ToListAsync();

        public async Task<Book> GetBookByISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return null;

            return await _ctx.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<List<Book>> SearchBooks(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new List<Book>();

            return await _ctx.Books
                .Where(b => b.Title.Contains(term) || b.Author.Contains(term))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _ctx.Books.Update(book);
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> IsBookAvailable(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            var book = await GetBookByISBN(isbn);
            return book != null && book.AvailableCopies > 0;
        }
    }
}