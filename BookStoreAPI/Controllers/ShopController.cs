﻿using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using BusinessObjects.DTO;

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
			filters.genres = repository.GenreAndQuantity();
			filters.publishers = repository.PublisherAndQuantity();
			filters.languages = repository.LanguageAndQuantity();
			filters.authors = repository.AuthorAndQuantity();

			return filters;
		}

		[HttpGet("Filter")]
		public ActionResult<IEnumerable<Book>> FilterByGenre([FromQuery] int filterName, [FromQuery] int filterId)
		{
			List<Book> booksFilter = new List<Book>();

			if (filterName == 1) booksFilter = repository.FilterByGenre(filterId);
			else if (filterName == 2) booksFilter = repository.FilterByPublisher(filterId);
			else if (filterName == 3) booksFilter = repository.FilterByLanguage(filterId);
			else if (filterName == 4) booksFilter = repository.FilterByAuthor(filterId);

			return booksFilter;
		}

		[HttpGet("Detail/{bookId}")]
		public ActionResult<BookDetail> Detail(int bookId)
		{
			return repository.BookDetail(bookId);
		}
		[HttpGet("Related")]
		public ActionResult<BookDetail> ByGenre(int genreId)
		{
			return repository.BookDetail(genreId);
		}
	}
}
