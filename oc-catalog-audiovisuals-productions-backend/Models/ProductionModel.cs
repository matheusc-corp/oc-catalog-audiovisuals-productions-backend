namespace oc_catalog_audiovisuals_productions_backend.Models
{
    public class ProductionModel
    {        
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string? AlternativeName { get; set; }
        public string Description { get; set; }
        public ICollection<ProductionGenreModel> ProductionsGenres { get; set; }
        public int ReleasedYear { get; set; }
    }
}

