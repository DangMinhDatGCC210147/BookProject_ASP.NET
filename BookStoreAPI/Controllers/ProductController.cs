using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;

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
        public async Task<IActionResult> PostProducts([FromForm] Book book)
        {
                APIResponse responses = new APIResponse();
                try
                {
                // Lưu sản phẩm vào cơ sở dữ liệu (đảm bảo rằng bạn đã có phương thức để thực hiện việc này)
                //var newProduct = new Book
                //{
                //    Title = book.Title,
                //    Description = book.Description,
                //    Quantity = book.Quantity,
                //    OriginalPrice = book.OriginalPrice,
                //    SellingPrice = book.SellingPrice,
                //    ISBN = book.ISBN,
                //    PageCount = book.PageCount,
                //    IsSale = book.IsSale,
                //    PublicationYear = book.PublicationYear,
                //    PublisherId = book.PublisherId,
                //    LanguageId = book.LanguageId,
                //    AuthorId = book.AuthorId,
                //    GenreId = book.GenreId,
                //    Image = null,
                //};

                // Gọi phương thức để thêm sản phẩm mới vào cơ sở dữ liệu
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
        public IActionResult UpdateProducts([FromForm] int id, Book book)
        {
            var checkProduct = repository.GetProductById(id);
            if (checkProduct == null)
                return NotFound();
            book.Id = id;
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

    }
}
