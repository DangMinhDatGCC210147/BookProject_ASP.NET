using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/Discounts")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository repository = new DiscountRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Discount>> GetDiscounts() => repository.GetDiscounts();

        [HttpGet("{id}")]
        public ActionResult<Discount> GetDiscountById(int id)
        {
            var discount = repository.GetDiscountById(id);
            if (discount == null)
                return NotFound();

            return Ok(discount);
        }

        [HttpPost]
        public IActionResult CreateDiscount([FromBody] Discount discount)
        {
            return Ok(repository.SaveDiscount(discount));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var discount = repository.GetDiscountById(id);
            if (discount == null)
                return NotFound();
            repository.DeleteDiscountById(discount);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDiscount(int id, Discount discount)
        {
            var existingDiscount = repository.GetDiscountById(id);
            if (existingDiscount == null)
                return NotFound();

            discount.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdateDiscount(discount));
        }
    }
}
