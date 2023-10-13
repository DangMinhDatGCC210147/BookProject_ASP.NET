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
        private ICartDetailRepository cartRepository = new CartDetailRepository();
        private ICartDetailRepository userRepository = new CartDetailRepository();

        [HttpGet("{userId}")]
		public IActionResult GetCartDetails(string userId)
		{
            return Ok(cartRepository.GetCartDetails(userId));
		}

		[HttpPost("{userId}")]
        public IActionResult SaveCartDetails(CartDetail cartDetail, string userId)
        {
			CartDetail foundCart = cartRepository.FindBookInCart(cartDetail.BookId, userId);
            if (foundCart == null) return Ok(cartRepository.SaveCartDetail(cartDetail));
            else
            {
                cartDetail.Id = foundCart.Id;
                cartDetail.Quantity += foundCart.Quantity;
                var cd = cartRepository.UpdateCartDetail(cartDetail);
				return Ok(cd);
			}
        }

        [HttpDelete()]
        public IActionResult DeleteBookInCart(CartQuantity cart)
        {
            cartRepository.DeleteCartDetailById(cart.bookId, cart.userId);
            return Ok();
        }

        [HttpPut()]
        public IActionResult UpdateCartDetails(CartQuantity updateQuantity)
        {
            var checkCartDetail = cartRepository.FindBookInCart(updateQuantity.bookId, updateQuantity.userId);
            if (checkCartDetail == null)
                return NotFound();
            checkCartDetail.Quantity = updateQuantity.newQuantity;
			cartRepository.UpdateCartDetail(checkCartDetail);
            return Ok();
        }

		[HttpPost("CheckOut")]
		public IActionResult DisplayCheckOut(string userId)
		{
            CheckOut checkOut = new CheckOut();
            checkOut.ListBooks = cartRepository.GetCartDetails(userId);
			return Ok(checkOut);
		}
	}
}
