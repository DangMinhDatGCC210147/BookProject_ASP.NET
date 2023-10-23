using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using BusinessObjects.DTO;
using System.Net;

namespace BookStoreAPI.Controllers
{
	[Route("api/Shops")]
	[ApiController]
	public class ShopController : Controller
	{
		private readonly IShopRepository repository = new ShopRepository();

		[HttpGet]
		public async Task<ActionResult<IEnumerable<BookList>>> GetProducts()
		{
			List<BookList> books = await repository.GetProducts();
			return Ok(books);
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<BookList>>> GetProductsByFavoutite(string userId)
		{
			List<BookList> books = await repository.GetProductsByFavoutite(userId);
			return Ok(books);
		}

		[HttpGet("NavBar")]
		public ActionResult<Filter> GetFilter()
		{
			Filter filters = new Filter();
			filters.Genres = repository.GenreAndQuantity();
			filters.Publishers = repository.PublisherAndQuantity();
			filters.Languages = repository.LanguageAndQuantity();
			filters.Authors = repository.AuthorAndQuantity();

			return filters;
		}

		[HttpGet("Filter")]
		public ActionResult<IEnumerable<BookList>> FilterByGenre([FromQuery] int filterName, [FromQuery] int filterId)
		{
			List<BookList> booksFilter = new List<BookList>();

			if (filterName == 1) booksFilter = repository.FilterByGenre(filterId);
			else if (filterName == 2) booksFilter = repository.FilterByPublisher(filterId);
			else if (filterName == 3) booksFilter = repository.FilterByLanguage(filterId);
			else if (filterName == 4) booksFilter = repository.FilterByAuthor(filterId);

			return booksFilter;
		}

		[HttpGet("Detail")]
		public async Task<ActionResult<BookDetail>> DetailAsync(int bookId, string userId)
		{
			BookDetail bookDetail;

			if (userId == "null")
			{
				bookDetail = await repository.BookDetail(bookId);
			}
			else
			{
				bookDetail = await repository.BookDetailIsFavourite(bookId, userId);
			}

			return Ok(bookDetail);
		}

		[HttpGet("Related")]
		public async Task<ActionResult<IEnumerable<BookList>>> ByGenre(int genreId, string userId)
		{
            if (userId == null)
            {
                userId = "null";
            }
            List<BookList> bookDetail = await repository.RelatedBook(genreId, userId);
			return Ok(bookDetail);
		}
	}
}
