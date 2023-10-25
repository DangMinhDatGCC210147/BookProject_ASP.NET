using Azure;
using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		private readonly IProductRepository repository = new ProductRepository();
		private readonly IWebHostEnvironment environment;
		public ProductController(IWebHostEnvironment environment)
		{
			this.environment = environment;
		}

		[HttpPut("Quantity/Update")]
		public IActionResult GetBookById([FromBody] BookQuantity bookQuantity)
		{
			bool book = repository.UpdateQuantity(bookQuantity.BookId, bookQuantity.SoldQuantity);
			if (book)
				return Ok();
			return NotFound();

		}

		[HttpGet]
		public ActionResult<IEnumerable<BookView>> GetProducts() => repository.GetProducts();

		[HttpGet("{id}")]
		public ActionResult<BookList> GetBookById(int id)
		{
			var book = repository.GetProductById(id);
			if (book == null)	
				return NotFound();

			return Ok(book);
		}

		[HttpGet("Search")]
		public async Task<ActionResult<List<BookList>>> Search(string name, string userId)
		{
			return Ok(await repository.GetProductByName(name, userId));
		}

		[HttpPost]
		public async Task<IActionResult> PostProducts([FromForm] Book book)
		{
			APIResponse responses = new APIResponse();
			try
			{
				
				repository.SaveProduct(book);

				// Tiếp theo, lưu hình ảnh theo cách bạn đã thực hiện trong phương thức gốc của bạn
				string Filepath = GetFilepath(book.Title);
				if (!System.IO.Directory.Exists(Filepath))
				{
					System.IO.Directory.CreateDirectory(Filepath);
				}
				string imagepath = Filepath + "\\" + book.Title + ".png";
				if (!System.IO.File.Exists(imagepath))
				{
					System.IO.File.Delete(imagepath);
				}

				using (FileStream stream = System.IO.File.Create(imagepath))
				{
					await book.ImageFile.CopyToAsync(stream);
					responses.ResponseCode = 200;
					responses.Result = "pass";
				}
				book.Image = "upload\\" + book.Title + "\\" + book.Title + ".png";


				return Ok(repository.UpdateProduct(book));
			}
			catch (Exception ex)
			{
				responses.Errormessage = ex.Message;
			}
			return Ok();
		}


		[HttpDelete("{id}")]
		public IActionResult DeleteProducts(int id)
		{
			var product = repository.GetProductById(id);
			if (product == null)
				return NotFound();
			repository.DeleteProductById(product);
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateProducts( int id,[FromForm] Book book)
     	{
			var checkProduct = repository.GetProductById(id);
			if (checkProduct == null)
				return NotFound();
			book.Id = id;
			if (book.ImageFile != null)
			{
				// Lấy đường dẫn đến thư mục lưu ảnh mới
				string filePath = GetFilepath(book.Title);

				if (!System.IO.Directory.Exists(filePath))
				{
					System.IO.Directory.CreateDirectory(filePath);
				}

				// Kiểm tra xem có sẵn ảnh cũ không
				string existingImagePath = filePath + "\\" + book.Title + ".png";
				if (System.IO.File.Exists(existingImagePath))
				{
					System.IO.File.Delete(existingImagePath);
				}

				using (FileStream stream = System.IO.File.Create(existingImagePath))
				{
					await book.ImageFile.CopyToAsync(stream);
				}

				book.Image = "upload\\" + book.Title + "\\" + book.Title + ".png";
			}
			else
			{
				// Nếu không có ảnh mới, giữ nguyên đường dẫn ảnh cũ
				book.Image = checkProduct.Image;
			}

			return Ok(repository.UpdateProduct(book));
		}
		[NonAction]
		private string GetFilepath(string title)
		{
			return this.environment.WebRootPath + "\\upload\\" + title;
		}
		[HttpGet("GetImage/{Title}")]
		public async Task<IActionResult> GetImage(string Title)
		{
			try
			{
				string Filepath = GetFilepath(Title);
				string imagepath = Filepath + "\\" + Title + ".png";
				if (System.IO.File.Exists(imagepath))
				{
					byte[] imageBytes = System.IO.File.ReadAllBytes(imagepath);
					string base64Image = Convert.ToBase64String(imageBytes);
					return Ok(new { Base64Image = base64Image });
				}
				else
				{
					return NotFound("Images is not available");
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi tại đây nếu cần.
				return StatusCode(500, "Error when accessing the image!");
			}
		}
		[HttpGet("GetAllTitles")]
		public IActionResult GetAllTitles()
		{
			try
			{
				// Sử dụng repository để lấy danh sách các Title (hoặc sản phẩm)
				var titles = repository.GetProducts().Select(product => product.Title).ToList();
				return Ok(titles);
			}
			catch (Exception ex)
			{
				// Xử lý lỗi ở đây nếu cần
				return StatusCode(500, "Error when accessing the titles.");
			}
		}

		[HttpGet("export")]
		public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
		{
			// query data from database  
			await Task.Yield();

			var list = repository.GetProducts().ToList();
			var stream = new MemoryStream();

			using (var package = new ExcelPackage(stream))
			{
				var workSheet = package.Workbook.Worksheets.Add("Product");
				workSheet.Cells.LoadFromCollection(list, true);
				package.Save();
			}
			stream.Position = 0;
			string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

			//return File(stream, "application/octet-stream", excelName);  
			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
		}

	}
}
