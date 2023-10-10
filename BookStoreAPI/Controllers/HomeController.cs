using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using DataAccess;

namespace BookStoreAPI.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeRepository repository = new HomeRepository();

        [HttpGet]
        public ActionResult<Filter> GetFilter()
        {
            
            
            return null;
        }

		[HttpGet("/Search")]
		public ActionResult<IEnumerable<Book>> FilterByGenre([FromQuery] int filterName, [FromQuery] int filterId)
		{
            List<Book> booksFilter = new List<Book>();

			

			return booksFilter;
		}
	}
}
