using SmartLibrary.DTOs.CatalogDTOs;

namespace SmartLibrary.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogResponseDTO> CreateCatalogAsync(CreateCatalogDTO dto);
        Task<CatalogResponseDTO> GetCatalogByIdAsync(string catalogId);
        Task<IEnumerable<CatalogResponseDTO>> GetAllCatalogsAsync();
        Task<CatalogResponseDTO> UpdateCatalogAsync(string catalogId, UpdateCatalogDTO dto);
        Task<bool> DeleteCatalogAsync(string catalogId);
    }
}