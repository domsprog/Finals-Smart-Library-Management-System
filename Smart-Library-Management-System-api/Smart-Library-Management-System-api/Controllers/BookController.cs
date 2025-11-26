using Microsoft.AspNetCore.Mvc;
using Smart_Library_Management_System_api.SmartLibrary.Dto;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // ================================
        // 1. GET ALL BOOKS
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return Ok(books);
        }

        // ================================
        // 2. GET BOOK BY ISBN
        // ================================
        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetBookByISBN(string isbn)
        {
            var book = await _bookRepository.GetBookByISBN(isbn);

            if (book == null)
                return NotFound($"Book with ISBN {isbn} not found");

            return Ok(book);
        }

        // ================================
        // 3. SEARCH BOOKS (title/author)
        // ================================
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string term)
        {
            var results = await _bookRepository.SearchBooks(term);

            return Ok(results);
        }

        // ================================
        // 4. CREATE A NEW BOOK
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookRequest request)
        {
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

            await _bookRepository.AddBook(book);

            return CreatedAtAction(nameof(GetBookByISBN), new { isbn = book.ISBN }, book);
        }

        // ================================
        // 5. UPDATE BOOK INFO
        // ================================
        [HttpPut("{isbn}")]
        public async Task<IActionResult> UpdateBook(string isbn, UpdateBookRequest request)
        {
            var book = await _bookRepository.GetBookByISBN(isbn);

            if (book == null)
                return NotFound($"Book with ISBN {isbn} not found");

            // Update properties
            book.Title = request.Title ?? book.Title;
            book.Author = request.Author ?? book.Author;
            book.Publisher = request.Publisher ?? book.Publisher;
            book.Category = request.Category ?? book.Category;
            book.Price = (decimal)(request.Price >= 0 ? request.Price : book.Price);

            await _bookRepository.UpdateBook(book);

            return Ok(book);
        }

        // ================================
        // 6. DELETE BOOK
        // ================================
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteBook(string isbn)
        {
            var book = await _bookRepository.GetBookByISBN(isbn);

            if (book == null)
                return NotFound($"Book with ISBN {isbn} not found");

            await _bookRepository.DeleteBook(isbn);

            return Ok($"Book with ISBN {isbn} deleted successfully");
        }

        // ================================
        // 7. CHECK IF BOOK AVAILABLE
        // ================================
        [HttpGet("{isbn}/available")]
        public async Task<IActionResult> IsBookAvailable(string isbn)
        {
            var available = await _bookRepository.IsBookAvailable(isbn);

            return Ok(new { ISBN = isbn, IsAvailable = available });
        }

    }
}
