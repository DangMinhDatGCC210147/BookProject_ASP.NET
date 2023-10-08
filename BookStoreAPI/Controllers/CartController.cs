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
        public ActionResult<Cart> GetCartById(string id)
        {
            var Cart = repository.GetUserCart(id);
            if (Cart == null)
                return NotFound();

            return Ok(Cart);
        }

		[HttpPost]
		public IActionResult PostCarts(Cart Cart)
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

		[HttpPut("id")]
		public IActionResult UpdateCarts(int id, Cart Cart)
		{
			var checkCart = repository.FindCartById(id);
			if (checkCart == null)
				return NotFound();
			repository.UpdateCart(Cart);
			return Ok();
		}
	}
}
