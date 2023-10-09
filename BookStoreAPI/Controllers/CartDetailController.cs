using BookStore.Models;
using BusinessObjects;
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

        [HttpPost]
        public IActionResult SaveCartDetails(CartDetail CartDetail)
        {
            repository.SaveCartDetail(CartDetail);
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteCartDetails(int id)
        {
            CartDetail cartdetail = repository.FindCartDetailById(id);
            if (cartdetail == null)
                return NotFound();
            repository.DeleteCartDetailById(cartdetail);
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateCartDetails(int id, CartDetail CartDetail)
        {
            var checkCartDetail = repository.FindCartDetailById(id);
            if (checkCartDetail == null)
                return NotFound();
            repository.UpdateCartDetail(CartDetail);
            return Ok();
        }
    }
}
