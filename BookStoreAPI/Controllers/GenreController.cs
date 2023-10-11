using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

namespace BookStoreAPI.Controllers
{
    [Route("api/Genres")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository repository = new GenreRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetGenres() => repository.GetGenres();

        [HttpGet("{id}")]
        public ActionResult<Genre> GetGenreById(int id)
        {
            var genre = repository.GetGenreById(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        public IActionResult CreateGenre(Genre genre)
        {
            repository.SaveGenre(genre);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenres(int id)
        {
            var genre = repository.GetGenreById(id);
            if (genre == null)
                return NotFound();
            repository.DeleteGenreById(genre);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, Genre genre)
        {
            var existingGenre = repository.GetGenreById(id);
            if (existingGenre == null)
                return NotFound();

            genre.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateGenre(genre);
            return Ok();
        }
    }
}
