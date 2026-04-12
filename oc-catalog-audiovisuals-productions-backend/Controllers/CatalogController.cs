using Microsoft.AspNetCore.Mvc;
using oc_catalog_audiovisuals_productions_backend.Context;
using oc_catalog_audiovisuals_productions_backend.DTOs;
using oc_catalog_audiovisuals_productions_backend.Models;
using oc_catalog_audiovisuals_productions_backend.Services;

namespace oc_catalog_audiovisuals_productions_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        CatalogServices _catalogService = new CatalogServices();
        private readonly CatalogContext _catalogContext;

        public CatalogController(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        [HttpPost]
        public IActionResult InsertProduction(ProductionDto productionDto)
        {
            var production = new ProductionModel
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = productionDto.Name,
                Description = productionDto.Description,
                ReleasedYear = productionDto.ReleasedYear,
                ProductionsGenres = productionDto.GenresIds.Select(genreId => new ProductionGenreModel
                {
                    GenreId = genreId
                }).ToList()
            };

            _catalogContext.Productions.Add(production);
            _catalogContext.SaveChanges();

            var response = new ProductionResponseDto
            {
                Id = production.Id,
                Name = production.Name,
                AlternativeName = production.AlternativeName,
                Description = production.Description,
                ReleasedYear = production.ReleasedYear,
                GenresIds = production.ProductionsGenres.Select(p => p.GenreId).ToList()

            };

            return Ok(response);
        }

        [HttpPut("id/{id}")]
        public IActionResult EditProduction(int id)
        {
            ProductionModel foundProduction = _catalogContext.Productions.Find(id);

            if(foundProduction == null)
                return NotFound();

            _catalogContext.Productions.Update(foundProduction);
            _catalogContext.SaveChanges();
            return Ok(foundProduction);
        }

        [HttpDelete("id/{id}")]
        public IActionResult DeleteProduction(int id)
        {
            ProductionModel foundProduction = _catalogContext.Productions.Find(id);

            if(foundProduction == null)
                return NotFound();

            _catalogContext.Remove(foundProduction);
            _catalogContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("id/{id}")]
        public IActionResult GetProductionById(int id)
        {
            ProductionModel foundProdutction = _catalogContext.Productions.Find(id);

            if(foundProdutction == null)
                return NotFound();

            return Ok(foundProdutction);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetProductionByName(string name)
        {
            var foundProdutctions = _catalogContext.Productions.Where(x => x.Name.Contains(name));

            if (foundProdutctions == null)
                return NotFound();

            return Ok(foundProdutctions);
        }

        [HttpGet("year/{year}")]
        public IActionResult GetProductionByYear(int year)
        {
            var foundProdutctions = _catalogContext.Productions.Where(x => x.ReleasedYear == year);

            if (foundProdutctions == null)
                return NotFound();

            return Ok(foundProdutctions);
        }

        [HttpGet("genre/{genre}")]
        public IActionResult GetProductionByGenre(string genre)
        {
            //var foundProdutction = _catalogContext.Productions.Where(x => x.Genre.ToString() == genre);

            //if (foundProdutction == null)
            //    return NotFound();

            //return Ok(foundProdutction);
            return null;
        }
    }
}
