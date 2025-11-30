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
        public async Task<IActionResult> Create([FromBody] CreateBookDTO dto)
        {
            try
            {
                var book = await _bookService.CreateBook(dto);
                return CreatedAtAction(nameof(Get), new { isbn = book.ISBN }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{isbn}")]
        public async Task<IActionResult> Update(string isbn, [FromBody] UpdateBookDTO dto)
        {
            try
            {
                var book = await _bookService.UpdateBook(isbn, dto);
                if (book == null)
                    return NotFound($"Book with ISBN {isbn} not found.");
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{isbn}")]
        public async Task<IActionResult> Delete(string isbn)
        {
            try
            {
                var result = await _bookService.DeleteBook(isbn);
                if (!result)
                    return NotFound($"Book with ISBN {isbn} not found.");
                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> Get(string isbn)
        {
            var book = await _bookService.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound($"Book with ISBN {isbn} not found.");
            }
            return Ok(book); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        
        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var books = await _bookService.SearchBooks(searchTerm);
            return Ok(books);
        }

        
        [HttpGet("available/{isbn}")]
        public async Task<IActionResult> CheckAvailability(string isbn)
        {
            var isAvailable = await _bookService.IsBookAvailable(isbn);
            return Ok(new { ISBN = isbn, IsAvailable = isAvailable });
        }
    }
}