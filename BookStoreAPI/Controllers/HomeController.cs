using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using BusinessObjects.DTO;

namespace BookStoreAPI.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeRepository repository = new HomeRepository();

        [HttpGet]
        public ActionResult<IEnumerable<BookAuthor>> GetFilter()
        {
            try
            {
                // Call the TopSixAuthors method from your service or data access layer.
                var topAuthors = repository.GetBookAuthors();

                if (topAuthors != null)
                {
                    return Ok(topAuthors);
                }
                else
                {
                    // Handle the case when no data is found.
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors and return an appropriate response.
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
