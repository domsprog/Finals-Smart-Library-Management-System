namespace SmartLibrary.DTOs.CatalogDTOs
{
   

    public class CreateCatalogDTO
    {
       
        public string CatalogId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> BookISBNs { get; set; } = new List<string>();
    }

    public class UpdateCatalogDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> BookISBNs { get; set; }
    }

    public class CatalogResponseDTO
    {
        public int Id { get; set; } 
        public string CatalogId { get; set; } 
        public string Category { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public List<string> BookISBNs { get; set; } = new List<string>(); 
    }
}
