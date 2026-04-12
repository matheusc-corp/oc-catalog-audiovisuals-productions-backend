namespace oc_catalog_audiovisuals_productions_backend.DTOs
{
    public class ProductionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? AlternativeName { get; set; }
        public string Description { get; set; }
        public List<int> GenresIds { get; set; }
        public int ReleasedYear { get; set; }
    }
}
