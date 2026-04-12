using Microsoft.EntityFrameworkCore;
using oc_catalog_audiovisuals_productions_backend.Models;

namespace oc_catalog_audiovisuals_productions_backend.Context
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        public DbSet<ProductionModel> Productions { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<ProductionGenreModel> ProductionsGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductionGenreModel>()
                .HasKey(fg => new { fg.ProductionId, fg.GenreId });

            modelBuilder.Entity<ProductionGenreModel>()
                .HasOne(fg => fg.Production)
                .WithMany(fg => fg.ProductionsGenres)
                .HasForeignKey(fg => fg.ProductionId);

            modelBuilder.Entity<ProductionGenreModel>()
                .HasOne(fg => fg.Genre)
                .WithMany(fg => fg.ProductionsGenres)
                .HasForeignKey(fg => fg.GenreId);
        }
    }
}
