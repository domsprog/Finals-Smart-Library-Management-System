using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.CatalogDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepo;
        public CatalogService(ICatalogRepository catalogRepo) => _catalogRepo = catalogRepo;

        public async Task<CatalogResponseDTO> CreateCatalogAsync(CreateCatalogDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Category))
                throw new ArgumentException("Category is required", nameof(dto.Category));

            var c = new Catalog
            {
                CatalogId = dto.CatalogId ?? Guid.NewGuid().ToString(), 
                Category = dto.Category,
                Name = dto.Name, 
                Description = dto.Description, 
                BookISBNs = dto.BookISBNs ?? new List<string>() 
            };

            var added = await _catalogRepo.AddCatalog(c);
            return new CatalogResponseDTO
            {
                Id = added.Id, 
                CatalogId = added.CatalogId, 
                Category = added.Category,
                Name = added.Name, 
                Description = added.Description, 
                BookISBNs = added.BookISBNs 
            };
        }

        public async Task<IEnumerable<CatalogResponseDTO>> GetAllCatalogsAsync()
        {
            var list = await _catalogRepo.GetAllCatalogs();
            return list.Select(c => new CatalogResponseDTO
            {
                Id = c.Id, 
                CatalogId = c.CatalogId, 
                Category = c.Category,
                Name = c.Name, 
                Description = c.Description, 
                BookISBNs = c.BookISBNs 
            });
        }


        public async Task<CatalogResponseDTO> GetCatalogByIdAsync(string catalogId)
        {
            if (string.IsNullOrWhiteSpace(catalogId)) return null;
            var c = await _catalogRepo.GetCatalogById(catalogId);
            if (c == null) return null;

            return new CatalogResponseDTO
            {
                Id = c.Id,
                CatalogId = c.CatalogId,
                Category = c.Category,
                Name = c.Name,
                Description = c.Description,
                BookISBNs = c.BookISBNs
            };
        }

        public async Task<CatalogResponseDTO> UpdateCatalogAsync(string catalogId, UpdateCatalogDTO dto)
        {
            if (string.IsNullOrWhiteSpace(catalogId))
                throw new ArgumentException("catalogId required");

            var c = await _catalogRepo.GetCatalogById(catalogId);
            if (c == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                c.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Category))
                c.Category = dto.Category;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                c.Description = dto.Description;

            if (dto.BookISBNs != null)
                c.BookISBNs = dto.BookISBNs;

            await _catalogRepo.UpdateCatalog(c);

            return new CatalogResponseDTO
            {
                Id = c.Id,
                CatalogId = c.CatalogId,
                Category = c.Category,
                Name = c.Name,
                Description = c.Description,
                BookISBNs = c.BookISBNs
            };
        }

        public async Task<bool> DeleteCatalogAsync(string catalogId)
        {
            if (string.IsNullOrWhiteSpace(catalogId)) return false;
            var existing = await _catalogRepo.GetCatalogById(catalogId);
            if (existing == null) return false;
            await _catalogRepo.DeleteCatalog(catalogId);
            return true;
        }
    }
}

