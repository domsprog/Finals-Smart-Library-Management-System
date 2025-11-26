namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Catalog
    {
        public string CatalogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> BookISBNs { get; set; } = new List<string>();
    }
}


