using BookStore.Models;
using BookStoreAPI.Controllers;
using BusinessObjects;
using BusinessObjects.Data.Enum;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using Repositories;
using Repositories.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreAPI.Controllers
{
    [Route("api/CartDetails")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private ICartDetailRepository cartRepository = new CartDetailRepository();

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

		//Payment
		[HttpGet("Total/{userId}")]
		public ActionResult<decimal> GetTotal(string userId)
		{
			var user = cartRepository.GetTotal(userId);
			if (user == null)
				return NotFound();
			return Ok(user);
		}

		[HttpDelete("ClearCart/{userId}")]
		public IActionResult ClearCartByUserId(string userId)
		{
			List<BookCart> bookCarts = cartRepository.GetCartDetails(userId);

            if (bookCarts != null)
            {
                foreach (var item in bookCarts)
                {
					cartRepository.DeleteCartDetailById(item.BookId, userId);
				}
				return Ok();
			}
			return NotFound();
		}
	}
}

