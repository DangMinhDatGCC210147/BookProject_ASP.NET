using BookStore.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace BookStoreAPI.Controllers
{
	[Route("api/Wishlists")]
	[ApiController]
	public class WishlistController : ControllerBase
	{
		private IWishlistRepository repository = new WishlistRepository();

		[HttpGet("{userId}")]
		public IActionResult GetWishlists(string userId)
		{
			return Ok(repository.GetWishlists(userId));
		}

		[HttpPost]
		public IActionResult SaveWishlists(Favourite favourite)
		{
			var found = repository.FindWishlistById(favourite.BookId, favourite.UserId);
            if (found != null)
				return NotFound();
			repository.SaveWishlist(favourite);
			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteWishlists(Favourite favourite)
		{
			var found = repository.FindWishlistById(favourite.BookId, favourite.UserId);
            if (found == null)
				return NotFound();
            repository.DeleteWishlistById(found);
			return Ok();
		}
	}
}
