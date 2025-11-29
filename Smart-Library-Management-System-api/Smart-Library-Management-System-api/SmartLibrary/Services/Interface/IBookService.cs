using SmartLibrary.DTOs.BookDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookResponseDTO> CreateBook(CreateBookDTO dto);
        Task<BookResponseDTO> UpdateBook(string isbn, UpdateBookDTO dto);
        Task<bool> DeleteBook(string isbn);
        Task<BookResponseDTO> GetBookByISBN(string isbn);
        Task<IEnumerable<BookResponseDTO>> GetAllBooks();
    }
}