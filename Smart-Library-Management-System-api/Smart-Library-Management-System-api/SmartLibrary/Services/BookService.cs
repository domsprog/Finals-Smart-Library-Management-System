using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.BookDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<BookResponseDTO> CreateBook(CreateBookDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.ISBN))
                throw new ArgumentException("ISBN is required", nameof(dto.ISBN));

            var existing = await _bookRepo.GetBookByISBN(dto.ISBN);
            if (existing != null)
                throw new InvalidOperationException($"Book with ISBN '{dto.ISBN}' already exists");

            var book = new Book
            {
                ISBN = dto.ISBN,
                Title = dto.Title,
                Author = dto.Author,
                Category = dto.Category ?? "Uncategorized",
                Publisher = dto.Publisher,
                PublicationYear = dto.PublicationYear,
                Price = dto.Price, 
                TotalCopies = dto.TotalCopies, 
                AvailableCopies = dto.AvailableCopies 
            };

            await _bookRepo.AddBook(book);

            return new BookResponseDTO
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher, 
                PublicationYear = book.PublicationYear, 
                Category = book.Category,
                Price = book.Price, 
                TotalCopies = book.TotalCopies, 
                AvailableCopies = book.AvailableCopies, 
                IsAvailable = book.IsAvailable 
            };
        }

        public async Task<bool> DeleteBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN is required", nameof(isbn));

            var existing = await _bookRepo.GetBookByISBN(isbn);
            if (existing == null)
                return false;

            await _bookRepo.DeleteBook(isbn);
            return true;
        }

        public async Task<IEnumerable<BookResponseDTO>> GetAllBooks()
        {
            var books = await _bookRepo.GetAllBooks();

            return books.Select(b => new BookResponseDTO
            {
                ISBN = b.ISBN,
                Title = b.Title,
                Author = b.Author,
                Publisher = b.Publisher, 
                PublicationYear = b.PublicationYear, 
                Category = b.Category,
                Price = b.Price, 
                TotalCopies = b.TotalCopies, 
                AvailableCopies = b.AvailableCopies, 
                IsAvailable = b.IsAvailable 
            });
        }

        public async Task<BookResponseDTO> GetBookByISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN is required", nameof(isbn));

            var book = await _bookRepo.GetBookByISBN(isbn);
            if (book == null)
                return null;

            return new BookResponseDTO
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher, 
                PublicationYear = book.PublicationYear, 
                Category = book.Category,
                Price = book.Price, 
                TotalCopies = book.TotalCopies, 
                AvailableCopies = book.AvailableCopies, 
                IsAvailable = book.IsAvailable 
            };
        }

        public async Task<BookResponseDTO> UpdateBook(string isbn, UpdateBookDTO dto)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("ISBN is required", nameof(isbn));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var book = await _bookRepo.GetBookByISBN(isbn);
            if (book == null)
                return null;

           
            if (!string.IsNullOrWhiteSpace(dto.Title))
                book.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Author))
                book.Author = dto.Author;

            if (!string.IsNullOrWhiteSpace(dto.Publisher))
                book.Publisher = dto.Publisher;

            if (!string.IsNullOrWhiteSpace(dto.Category))
                book.Category = dto.Category;

            if (dto.Price > 0)
                book.Price = dto.Price;

            if (dto.TotalCopies >= 0) 
                book.TotalCopies = dto.TotalCopies;

            if (dto.AvailableCopies >= 0) 
                book.AvailableCopies = dto.AvailableCopies;

            await _bookRepo.UpdateBook(book);

            return new BookResponseDTO
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Publisher = book.Publisher,
                PublicationYear = book.PublicationYear,
                Category = book.Category,
                Price = book.Price,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                IsAvailable = book.IsAvailable
            };
        }

       
        public async Task<IEnumerable<BookResponseDTO>> SearchBooks(string searchTerm)
        {
            var books = await _bookRepo.SearchBooks(searchTerm);
            return books.Select(b => new BookResponseDTO
            {
                ISBN = b.ISBN,
                Title = b.Title,
                Author = b.Author,
                Publisher = b.Publisher,
                PublicationYear = b.PublicationYear,
                Category = b.Category,
                Price = b.Price,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                IsAvailable = b.IsAvailable
            });
        }

        public async Task<bool> IsBookAvailable(string isbn)
        {
            return await _bookRepo.IsBookAvailable(isbn);
        }
    }
}