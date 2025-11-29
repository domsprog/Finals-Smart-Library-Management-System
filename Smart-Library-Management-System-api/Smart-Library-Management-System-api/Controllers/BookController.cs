using Microsoft.AspNetCore.Mvc;
using SmartLibrary.DTOs.BookDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBookDTO dto)
        {
            await _bookService.CreateBook(dto);
            return Ok("Book created");
        }

        [HttpPut("update/{isbn}")]
        public async Task<IActionResult> Update(string isbn, UpdateBookDTO dto)
        {
            await _bookService.UpdateBook(isbn, dto);
            return Ok("Book updated");
        }

        [HttpDelete("delete/{isbn}")]
        public async Task<IActionResult> Delete(string isbn)
        {
            await _bookService.DeleteBook(isbn);
            return Ok("Book deleted");
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> Get(string isbn)
        {
            var book = await _bookService.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound($"Book with ISBN {isbn} not found.");
            }
            if (string.IsNullOrEmpty(book.ISBN))
            {
                return BadRequest($"Book found, but ISBN is missing or invalid.");
            }
            return Ok(book.ISBN);  // Returns the ISBN as a string
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
    }
}
