using BookStore.Models;
using BusinessObjects;
using BusinessObjects.Data.Enum;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly HttpClient client = null;
		private string ProductApiUrl = "";
		private string ShopApiUrl = "";
		private string CartDetailApiUrl = "";
		private string WishlistApiUrl = "";
		private string UserApiUrl = "";
		private string HomeApiUrl = "";

		public HomeController(IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			client = new HttpClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			ProductApiUrl = "/api/Products";
			ShopApiUrl = "/api/Shops";
			CartDetailApiUrl = "/api/CartDetails";
			WishlistApiUrl = "/api/Wishlists";
			UserApiUrl = "/api/Profile";
			HomeApiUrl = "/api/Home";
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Index()
		{
			ViewData["api"] = _configuration["BaseAddress"];
			AppUser user = await _userManager.GetUserAsync(User);

			HttpResponseMessage httpResponse;
			if (user == null)
			{
				httpResponse = await client.GetAsync(HomeApiUrl);
			}
			else
			{
				httpResponse = await client.GetAsync(HomeApiUrl + "/" + user.Id);
			}

			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			BookHome bookHome = JsonSerializer.Deserialize<BookHome>(data, options);
			return View(bookHome);
		}

		[Route("/Home/FilterGenre/{filterId:int}", Name = "displaybookbygenre")]
		public async Task<IActionResult> Filter(string filterId)
		{
			ViewData["api"] = _configuration["BaseAddress"];

			HttpResponseMessage httpFilterResponse = await client.GetAsync(ShopApiUrl + "/NavBar");
			string dataFilter = await httpFilterResponse.Content.ReadAsStringAsync();
			var optionsFilter = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			Filter filter = JsonSerializer.Deserialize<Filter>(dataFilter, optionsFilter);

			HttpResponseMessage httpResponse = await client.GetAsync(ShopApiUrl + "/Filter?filterName=1" + "&filterId=" + filterId);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<BookList> books = JsonSerializer.Deserialize<List<BookList>>(data, options);

			ShopView shopView = new ShopView();
			shopView.Filter = filter;   // Filter in nav
			shopView.Books = books; // found book

			return View("Shop", shopView);
		}

		public async Task<IActionResult> Shop()
		{
			ViewData["api"] = _configuration["BaseAddress"];

			AppUser user = await _userManager.GetUserAsync(User);
			HttpResponseMessage httpFilterResponse = await client.GetAsync(ShopApiUrl + "/NavBar");
			string dataFilter = await httpFilterResponse.Content.ReadAsStringAsync();
			var optionsFilter = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			Filter filter = JsonSerializer.Deserialize<Filter>(dataFilter, optionsFilter);

			HttpResponseMessage httpResponse;
			if (user == null)
			{
				httpResponse = await client.GetAsync(ShopApiUrl);   //Book In shop without login
			}
			else
			{
				httpResponse = await client.GetAsync(ShopApiUrl + "/" + user.Id);   //Book In shop when login
			}
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<BookList> books = JsonSerializer.Deserialize<List<BookList>>(data, options);

			ShopView shopView = new ShopView();
			shopView.Filter = filter;
			shopView.Books = books;

			return View(shopView);
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
		public async Task<IActionResult> Help()
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

		public async Task<IActionResult> CheckOut(string userId)
		{
			HttpResponseMessage httpUserResponse = await client.GetAsync(UserApiUrl + "/" + userId);
			string user_data = await httpUserResponse.Content.ReadAsStringAsync();
			var user_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			AppUser users = JsonSerializer.Deserialize<AppUser>(user_data, user_options);
			ViewData["user"] = users;

			HttpResponseMessage httpCartResponse = await client.GetAsync(CartDetailApiUrl + "/" + userId);
			string cart_data = await httpCartResponse.Content.ReadAsStringAsync();
			var cart_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<BookCart> books = JsonSerializer.Deserialize<List<BookCart>>(cart_data, cart_options);
			ViewData["books"] = books;

			return View();
		}

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Profile()
		{
			AppUser user = await _userManager.GetUserAsync(User);
			HttpResponseMessage httpResponse = await client.GetAsync(UserApiUrl + "/" + user.Id);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			UserProfile userProfile = new UserProfile();
			userProfile.User = JsonSerializer.Deserialize<AppUser>(data, options);
			return View(userProfile);
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(UserProfile userProfile)
		{
			AppUser user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, userProfile.ChangePassword.OldPassword, userProfile.ChangePassword.NewPassword);
			if (!changePasswordResult.Succeeded)
			{
				foreach (var error in changePasswordResult.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				userProfile.User = user;
				return View("~/Views/Home/Profile.cshtml", userProfile);
			}

			await _signInManager.RefreshSignInAsync(user);
			return RedirectToAction("Profile", new { userId = user.Id });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}