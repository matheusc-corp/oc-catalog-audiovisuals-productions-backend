namespace oc_catalog_audiovisuals_productions_backend.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductionGenreModel> ProductionsGenres { get; set; }
    }
}
