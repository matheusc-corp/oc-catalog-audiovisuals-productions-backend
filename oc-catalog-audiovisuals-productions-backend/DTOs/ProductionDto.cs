using oc_catalog_audiovisuals_productions_backend.Models;

namespace oc_catalog_audiovisuals_productions_backend.DTOs
{
    public class ProductionDto
    {
        public string Name { get; set; }
        public string AlternativeName { get; set; }
        public string Description { get; set; }
        public List<int> GenresIds { get; set; }
        public int ReleasedYear { get; set; }
    }
}
