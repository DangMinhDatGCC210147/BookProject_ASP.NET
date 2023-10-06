using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Controllers
{
	public class UserController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly HttpClient client = null;
		private string UserApiUrl = "";

		public UserController(IConfiguration configuration)
		{
			_configuration = configuration;
			client = new HttpClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			UserApiUrl = "/api/Users";
		}
		public IActionResult Login()
		{
			return View("Login", "Home");
		}
		public IActionResult Register()
		{
			return View("Register", "Home");
		}
	}
}
