using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace BookStoreAPI.Controllers
{
	[Route("api/Products")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductRepository repository = new ProductRepository();

		[HttpGet]
		public ActionResult<IEnumerable<Book>> GetProducts() => repository.GetProducts();
				
		[HttpPost]
		public IActionResult PostProducts(Book product)
		{
			repository.SaveProduct(product);
			return Ok();
		}

		[HttpDelete("id")]
		public IActionResult DeleteProducts(int id)
		{
			var product = repository.GetProductById(id);
            if (product == null)
				return NotFound();
            repository.DeleteProductById(product);
			return Ok();
		}

		[HttpPut("id")]
		public IActionResult UpdateProducts(int id, Book product)
		{
			var checkProduct = repository.GetProductById(id);
			if (checkProduct == null)
				return NotFound();
			repository.UpdateProduct(product);
			return Ok();
		}
	}
}
