using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Interface
{
    public interface ICatalogRepository
    {
        Task<List<Catalog>> GetAllCatalogs();
        Task<Catalog> GetCatalogById(string id);
        Task<Catalog> AddCatalog(Catalog c);
        Task UpdateCatalog(Catalog c);
        Task DeleteCatalog(string id);
    }
}