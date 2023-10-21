using Azure;
using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Repositories;
using Repositories.Interfaces;
using System.IO;

namespace BookStoreAPI.Controllers
{
	[Route("api/Products")]
	[ApiController]
	public class ProductController : ControllerBase
	{
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
		public ActionResult<IEnumerable<Book>> GetProducts() => repository.GetProducts();

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = repository.GetProductById(id);
            if (book == null)
                return NotFound();

			return Ok(book);
		}

        [HttpGet("Search/{name}")]
        public ActionResult<IEnumerable<Book>> Search(string name)
        {
            return Ok(repository.GetProductByName(name));
        }

        [HttpPost]
        public IActionResult PostProducts([FromBody] Book book)
        {
			repository.SaveProduct(book);
			return Ok(book);
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
		public IActionResult UpdateProducts(int id, Book book)
		{
			var checkProduct = repository.GetProductById(id);
			if (checkProduct == null)
				return NotFound();
			return Ok(repository.UpdateProduct(book));
		}
	}
}
