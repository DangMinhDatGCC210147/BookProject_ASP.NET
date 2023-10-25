using BusinessObjects;
using BusinessObjects.Data.Enum;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;


namespace BookStoreWebClient.Areas.Owner.Controllers
{
    [Authorize(Roles = "StoreOwner")]
    [Area("Owner")]
    public class BooksController : Controller
    {
        //private readonly IWebHostEnvironment environment2;

        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BooksController(IConfiguration configuration)
        {
            //this.environment2 = environment2;

            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "/api/Products";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{

				ViewData["api"] = _configuration["BaseAddress"];
				HttpResponseMessage httpResponse = await client.GetAsync(ProductApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

				string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

				List<BookView> books = JsonSerializer.Deserialize<List<BookView>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).
				return View(books);
			}
			catch (IOException ex)
			{
                Console.Write(ex.Message);
                return View();
			}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book p)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Book> products = JsonSerializer.Deserialize<List<Book>>(strData, options);
            return View(products);
        }

        public ActionResult Detail()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id, Book book)
        {
            book.Id = id;
            string data = JsonSerializer.Serialize(book);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(ProductApiUrl + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products");
            }
            return View(book);
        }

    }
}
