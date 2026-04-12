namespace oc_catalog_audiovisuals_productions_backend.Models
{
    public class ProductionGenreModel
    {
        public int ProductionId { get; set; }
        public ProductionModel Production{ get; set; }
        public int GenreId { get; set; }
        public GenreModel Genre { get; set; }
    }
}
