using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

namespace BookStoreAPI.Controllers
{
    [Route("api/Discounts")]
    [ApiController]
    public class DiscountController : Controller
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
        public IActionResult CreateDiscount(Discount discount)
        {
            repository.SaveDiscount(discount);
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteDiscounts(int id)
        {
            var discount = repository.GetDiscountById(id);
            if (discount == null)
                return NotFound();
            repository.DeleteDiscountById(discount);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Discount discount)
        {
            var existingAuthor = repository.GetDiscountById(id);
            if (existingAuthor == null)
                return NotFound();

            discount.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateDiscount(discount);
            return Ok();
        }
    }
}
