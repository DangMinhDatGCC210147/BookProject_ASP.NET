using BookStore.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly HttpClient client = null;
		private string ProductApiUrl = "";

		public HomeController(IConfiguration configuration)
		{
			_configuration = configuration;
			client = new HttpClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			ProductApiUrl = "/api/Products";
		}

		public async Task<IActionResult> Index()
		{
			HttpResponseMessage httpResponse = await client.GetAsync(ProductApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

			string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Product> clubs = JsonSerializer.Deserialize<List<Product>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(clubs);
		}
		public async Task<IActionResult> Wishlist()
		{
			HttpResponseMessage httpResponse = await client.GetAsync(ProductApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

			string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			return View();
		}
		public async Task<IActionResult> Shop()
		{
			return View();
		}
		public async Task<IActionResult> Contact() 
		{
			return View();
		}
		public IActionResult AboutUs() 
		{
			return View();
		}
		public async Task<IActionResult> Cart()
		{
			return View();
		}

		public async Task<IActionResult> CheckOut()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Register() 
		{
			return View();
		}
		public IActionResult ProductDetail()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}