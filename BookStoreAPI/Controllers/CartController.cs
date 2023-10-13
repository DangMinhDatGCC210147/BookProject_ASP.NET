using BookStore.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace BookStoreAPI.Controllers
{
	[Route("api/Carts")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private ICartRepository repository = new CartRepository();

        [HttpPost]
		public IActionResult SaveCarts(string userId)
		{
			var cart = repository.FindCartById(userId);

			if (cart == null)
			{
                Cart c = new Cart();
                c.UserId = userId;
                c.CreatedDate = DateTime.Now;

                return Ok(repository.SaveCart(c));
            }

			return Ok(cart);
		}

		[HttpDelete("DeleteCarts/{userId}")]
		public IActionResult DeleteCarts(string userId)
		{
			var Cart = repository.FindCartById(userId);
            if (Cart == null)
				return NotFound();
            repository.DeleteCartById(Cart);
			return Ok();
		}
	}
}
