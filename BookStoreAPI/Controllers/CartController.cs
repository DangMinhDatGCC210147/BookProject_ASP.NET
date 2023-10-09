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

        [HttpGet("{id}")]
        public ActionResult<Cart> GetCartById(string userId)
        {
            var Cart = repository.GetCarts(userId);
            if (Cart == null)
                return NotFound();

            return Ok(Cart);
        }

        [HttpPost]
		public IActionResult SaveCarts(Cart Cart)
		{
			repository.SaveCart(Cart);
			return Ok();
		}

		[HttpDelete("id")]
		public IActionResult DeleteCarts(int id)
		{
			var Cart = repository.FindCartById(id);
            if (Cart == null)
				return NotFound();
            repository.DeleteCartById(Cart);
			return Ok();
		}
	}
}
