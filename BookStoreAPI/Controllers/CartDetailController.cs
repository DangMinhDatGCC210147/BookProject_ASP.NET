using BookStore.Models;
using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace BookStoreAPI.Controllers
{
    [Route("api/CartDetails")]
    [ApiController]
    public class CartDetailDetailController : ControllerBase
    {
        private ICartDetailRepository repository = new CartDetailRepository();

        [HttpGet("{userId}")]
		public IActionResult GetCartDetails(string userId)
		{
            return Ok(repository.GetCartDetails(userId));
		}

		[HttpPost("{userId}")]
        public IActionResult SaveCartDetails(CartDetail cartDetail, string userId)
        {
			CartDetail foundCart = repository.FindBookInCart(cartDetail.BookId, userId);
            if (foundCart == null) return Ok(repository.SaveCartDetail(cartDetail));
            else
            {
                cartDetail.Id = foundCart.Id;
                cartDetail.Quantity += foundCart.Quantity;
                var cd = repository.UpdateCartDetail(cartDetail);
				return Ok(cd);
			}
        }

        [HttpDelete()]
        public IActionResult DeleteBookInCart(CartQuantity cart)
        {
            repository.DeleteCartDetailById(cart.bookId, cart.userId);
            return Ok();
        }

        [HttpPut()]
        public IActionResult UpdateCartDetails(CartQuantity updateQuantity)
        {
            var checkCartDetail = repository.FindBookInCart(updateQuantity.bookId, updateQuantity.userId);
            if (checkCartDetail == null)
                return NotFound();
            checkCartDetail.Quantity = updateQuantity.newQuantity;
			repository.UpdateCartDetail(checkCartDetail);
            return Ok();
        }
    }
}
