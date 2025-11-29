//using Microsoft.AspNetCore.Mvc;
//using SmartLibrary.DTOs.CatalogDTOs;
//using SmartLibrary.Services.Interfaces;

//[Route("api/[controller]")]
//[ApiController]
//public class CatalogController : ControllerBase
//{
//    private readonly ICatalogService _service;

//    public CatalogController(ICatalogService service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAllCatalogs()
//    {
//        var catalogs = await _service.GetAllCatalogsAsync();
//        return Ok(catalogs);
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogDTO dto)
//    {
//        if (dto == null)
//        {
//            return BadRequest("Catalog data is required.");
//        }
//        try
//        {
//            var createdCatalog = await _service.CreateCatalogAsync(dto);
//            return CreatedAtAction(nameof(GetAllCatalogs), new { id = createdCatalog.CatalogId }, createdCatalog);
//        }
//        catch (ArgumentException ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}

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
                return CreatedAtAction(nameof(GetAllCatalogs), new { id = createdCatalog.CatalogId }, createdCatalog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}