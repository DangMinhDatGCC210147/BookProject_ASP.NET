using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

namespace BookStoreAPI.Controllers
{
    [Route("api/Authors")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository repository = new AuthorRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors() => repository.GetAuthors();

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = repository.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            repository.SaveAuthor(author);
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAuthors(int id)
        {
            var author = repository.GetAuthorById(id);
            if (author == null)
                return NotFound();
            repository.DeleteAuthorById(author);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            var existingAuthor = repository.GetAuthorById(id);
            if (existingAuthor == null)
                return NotFound();

            author.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateAuthor(author);
            return Ok();
        }
    }
}
