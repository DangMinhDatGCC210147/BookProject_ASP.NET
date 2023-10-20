using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using BusinessObjects.DTO;
using DataAccess;

namespace BookStoreAPI.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeRepository repository = new HomeRepository();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetFilter()
        {
			try
			{
				BookHome books = new()
				{
					TopAuthors = await repository.GetBookAuthors(null),
					TopGenres = await repository.GetBookGenres(null),
				};

				return Ok(books);
			}
			catch (Exception ex)
			{
				// Log the exception for debugging purposes
				Console.WriteLine($"An error occurred: {ex.Message}");
				return StatusCode(500); // Return a 500 Internal Server Error response for other exceptions
			}
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<BookAuthor>>> GetFilter(string userId)
		{
			try
			{
				BookHome books = new()
				{
					TopAuthors = await repository.GetBookAuthors(userId),
					TopGenres = await repository.GetBookGenres(userId),
				};

				return Ok(books);
			}
			catch (Exception ex)
			{
				// Log the exception for debugging purposes
				Console.WriteLine($"An error occurred: {ex.Message}");
				return StatusCode(500); // Return a 500 Internal Server Error response for other exceptions
			}
		}

		[HttpGet("Search")]
		public ActionResult<IEnumerable<Book>> FilterByGenre([FromQuery] int filterName, [FromQuery] int filterId)
		{
            List<Book> booksFilter = new List<Book>();

			return booksFilter;
		}
	}
}
