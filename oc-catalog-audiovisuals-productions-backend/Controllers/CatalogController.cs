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
                AlternativeName = productionDto.AlternativeName,
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
            var foundProduction = _catalogContext.Productions.FirstOrDefault(x => x.Id == id);

            if(foundProduction == null)
                return NotFound();

            var genres = _catalogContext.ProductionsGenres.Where(p => p.ProductionId == foundProduction.Id).ToList();
            
            ProductionResponseDto response = new ProductionResponseDto
            {
                Id = foundProduction.Id,
                Name = foundProduction.Name,
                AlternativeName = foundProduction.AlternativeName,
                Description = foundProduction.Description,
                ReleasedYear = foundProduction.ReleasedYear,
                GenresIds = genres.Select(g => g.GenreId).ToList()

            };

            return Ok(response);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetProductionByName(string name)
        {
            var foundProductions = _catalogContext.Productions.Where(x => x.Name.Contains(name));

            if (foundProductions == null)
                return NotFound();

            return Ok(foundProductions);
        }

        [HttpGet("year/{year}")]
        public IActionResult GetProductionByYear(int year)
        {
            var foundProductions = _catalogContext.Productions.Where(x => x.ReleasedYear == year);

            if (foundProductions == null)
                return NotFound();

            return Ok(foundProductions);
        }

        [HttpGet("genre/{genre}")]
        public IActionResult GetProductionByGenre(string genre)
        {
            //var foundProduction = _catalogContext.Productions.Where(x => x.Genre.ToString() == genre);

            //if (foundProduction == null)
            //    return NotFound();

            //return Ok(foundProduction);
            return null;
        }
    }
}
