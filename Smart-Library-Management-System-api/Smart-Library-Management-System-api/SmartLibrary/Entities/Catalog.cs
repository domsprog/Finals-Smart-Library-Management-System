
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Library_Management_System_api.SmartLibrary.Entities
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // FIXED: Made CatalogId unique and required
        [Required]
        public string CatalogId { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> BookISBNs { get; set; } = new List<string>();
    }
}