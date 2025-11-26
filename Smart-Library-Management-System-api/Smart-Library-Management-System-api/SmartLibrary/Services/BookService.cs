using Smart_Library_Management_System_api.SmartLibrary.Dto; 
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using Smart_Library_Management_System_api.SmartLibrary.Services.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> CreateBook(CreateBookRequest request)
        {
            // validation (entity does its own validation for required fields)
            var book = new Book
            {
                ISBN = request.ISBN,
                Title = request.Title,
                Author = request.Author,
                Publisher = request.Publisher,
                PublicationYear = request.PublicationYear,
                Category = request.Category,
                Price = request.Price,
                TotalCopies = request.TotalCopies,
                AvailableCopies = request.TotalCopies
            };

            await _bookRepo.AddBook(book);
            return book;
        }

        public async Task<bool> DeleteBook(string isbn)
        {
            var existing = await _bookRepo.GetBookByISBN(isbn);
            if (existing == null) return false;

            await _bookRepo.DeleteBook(isbn);
            return true;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepo.GetAllBooks();
        }

        public async Task<Book> GetBookByISBN(string isbn)
        {
            return await _bookRepo.GetBookByISBN(isbn);
        }

        public async Task<List<Book>> SearchBooks(string keyword)
        {
            return await _bookRepo.SearchBooks(keyword);
        }

        public async Task<Book> UpdateBook(string isbn, UpdateBookRequest request)
        {
            var book = await _bookRepo.GetBookByISBN(isbn);
            if (book == null) return null;
            // Apply updates only for non-null values (DTO may use nullable types)
            book.Title = string.IsNullOrWhiteSpace(request.Title) ? book.Title : request.Title;
            book.Author = string.IsNullOrWhiteSpace(request.Author) ? book.Author : request.Author;
            book.Publisher = string.IsNullOrWhiteSpace(request.Publisher) ? book.Publisher : request.Publisher;
            book.Category = string.IsNullOrWhiteSpace(request.Category) ? book.Category : request.Category;
            if (request.PublicationYear.HasValue)
                book.PublicationYear = request.PublicationYear.Value;
            if (request.Price.HasValue && request.Price.Value >= 0)
                book.Price = request.Price.Value;
            if (request.TotalCopies.HasValue)
            {
                var oldTotal = book.TotalCopies;
                var newTotal = request.TotalCopies.Value;
                // Adjust available copies relative to change in total copies
                var delta = newTotal - oldTotal;
                book.TotalCopies = newTotal;
                book.AvailableCopies = Math.Max(0, book.AvailableCopies + delta);
            }
            if (request.AvailableCopies.HasValue)
                book.AvailableCopies = request.AvailableCopies.Value;
            // Ensure AvailableCopies is valid: non-negative and <= TotalCopies
            book.AvailableCopies = Math.Max(0, Math.Min(book.AvailableCopies, book.TotalCopies));
            await _bookRepo.UpdateBook(book);
            return book;
        }
    }
}
