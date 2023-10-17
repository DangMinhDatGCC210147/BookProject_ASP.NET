using BookStore.Models;
using BusinessObjects;
using BusinessObjects.Data.Enum;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private string ShopApiUrl = "";
        private string CartDetailApiUrl = "";
        private string WishlistApiUrl = "";
        private string UserApiUrl = "";

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "/api/Products";
            ShopApiUrl = "/api/Shops";
            CartDetailApiUrl = "/api/CartDetails";
            WishlistApiUrl = "/api/Wishlists";
			UserApiUrl = "/api/Users";
		}

        public IActionResult Index()
        {
            ViewData["api"] = _configuration["BaseAddress"];
			return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
        
		public async Task<IActionResult> Search(string title)
		{
            ViewData["search"] = title;
            HttpResponseMessage httpResponse = await client.GetAsync(ProductApiUrl + "/Search/" + title); 
            string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<Book> books = JsonSerializer.Deserialize<List<Book>>(data, options);

            return RedirectToAction("Shop", "Home", books);
		}
		public async Task<IActionResult> Contact()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Cart(string userId)
        {
			HttpResponseMessage httpResponse = await client.GetAsync(CartDetailApiUrl + "/" + userId);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<BookCart> books = JsonSerializer.Deserialize<List<BookCart>>(data, options);
			return View(books);
        }

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Wishlist(string userId)
		{
			HttpResponseMessage httpResponse = await client.GetAsync(WishlistApiUrl + "/" + userId);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<BookFavourite> books = JsonSerializer.Deserialize<List<BookFavourite>>(data, options);
			return View(books);
		}

		public async Task<IActionResult> Detail(int id)
        {
            HttpResponseMessage httpResponse = await client.GetAsync(ShopApiUrl + "/Detail/" + id); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 
            string data = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            BookDetail detail = JsonSerializer.Deserialize<BookDetail>(data, options);
            return View(detail);
        }

        public async Task<IActionResult> CheckOut()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Profile(string userId)
		{
			HttpResponseMessage httpResponse = await client.GetAsync(UserApiUrl + "/" + userId);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			AppUser Users = JsonSerializer.Deserialize<AppUser>(data, options);
			return View(Users);
		}
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(string userId, AppUser user)
        {
            user.Id = userId;
            string data = JsonSerializer.Serialize(userId);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(UserApiUrl + "/" + userId, content);
            return RedirectToAction("Profile", userId);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userId, AppUser user)
        {
            HttpResponseMessage httpResponse = await client.GetAsync(UserApiUrl + "/" + userId);
            string data = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AppUser Users = JsonSerializer.Deserialize<AppUser>(data, options);
            return RedirectToAction("Profile", userId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}