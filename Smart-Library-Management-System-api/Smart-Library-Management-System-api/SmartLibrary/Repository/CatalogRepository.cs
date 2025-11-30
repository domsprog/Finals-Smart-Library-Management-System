using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Data;
using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;

namespace Smart_Library_Management_System_api.SmartLibrary.Repository.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly DbContextLibrary _ctx;
        public CatalogRepository(DbContextLibrary ctx) => _ctx = ctx;

        public async Task<Catalog> AddCatalog(Catalog c)
        {
          
            await _ctx.Catalogs.AddAsync(c);
            await _ctx.SaveChangesAsync();
            return c;
        }

        public async Task<List<Catalog>> GetAllCatalogs() =>
            await _ctx.Catalogs.AsNoTracking().ToListAsync();

   
        public async Task<Catalog> GetCatalogById(string id)
        {
           
            if (int.TryParse(id, out int numericId))
            {
                var byId = await _ctx.Catalogs.FirstOrDefaultAsync(c => c.Id == numericId);
                if (byId != null) return byId;
            }

          
            return await _ctx.Catalogs.FirstOrDefaultAsync(c => c.CatalogId == id);
        }

        public async Task UpdateCatalog(Catalog c)
        {
            _ctx.Catalogs.Update(c);
            await _ctx.SaveChangesAsync();
        }

       
        public async Task DeleteCatalog(string id)
        {
            var catalog = await GetCatalogById(id);
            if (catalog == null) return;
            _ctx.Catalogs.Remove(catalog);
            await _ctx.SaveChangesAsync();
        }
    }
}
   