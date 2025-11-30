using Microsoft.AspNetCore.Mvc;
using SmartLibrary.DTOs.CatalogDTOs;
using SmartLibrary.Services.Interfaces;

namespace Smart_Library_Management_System_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _service;

        public CatalogController(ICatalogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogs()
        {
            var catalogs = await _service.GetAllCatalogsAsync();
            return Ok(catalogs);
        }

        [HttpGet("{catalogId}")]
        public async Task<IActionResult> GetCatalogById(string catalogId)
        {
            var catalog = await _service.GetCatalogByIdAsync(catalogId);
            if (catalog == null)
                return NotFound($"Catalog with ID {catalogId} not found.");
            return Ok(catalog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Catalog data is required.");
            }
            try
            {
                var createdCatalog = await _service.CreateCatalogAsync(dto);
                return CreatedAtAction(nameof(GetCatalogById), new { catalogId = createdCatalog.CatalogId }, createdCatalog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{catalogId}")]
        public async Task<IActionResult> UpdateCatalog(string catalogId, [FromBody] UpdateCatalogDTO dto)
        {
            try
            {
                var updated = await _service.UpdateCatalogAsync(catalogId, dto);
                if (updated == null)
                    return NotFound($"Catalog with ID {catalogId} not found.");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{catalogId}")]
        public async Task<IActionResult> DeleteCatalog(string catalogId)
        {
            var result = await _service.DeleteCatalogAsync(catalogId);
            if (!result)
                return NotFound($"Catalog with ID {catalogId} not found.");
            return Ok("Catalog deleted successfully");
        }
    }
}
