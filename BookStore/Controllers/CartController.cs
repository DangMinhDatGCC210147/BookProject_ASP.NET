using BusinessObjects;
using BusinessObjects.Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Controllers
{
	[Authorize(Roles = "Customer")]
	public class CartController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly HttpClient client = null;
		private string cartUrl = "";
		private string cartDetailUrl = "";

		public CartController(IConfiguration configuration)
		{
			_configuration = configuration;
			client = new HttpClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			cartUrl = "/api/Carts";
			cartDetailUrl = "/api/CartDetails";
		}
		// GET: HomeController1
		public async Task<ActionResult> IndexAsync()
		{
			HttpResponseMessage httpResponse = await client.GetAsync(cartUrl);
			string data = await httpResponse.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			List<FeedBack> feedBacks = JsonSerializer.Deserialize<List<FeedBack>>(data, options);
			return View(feedBacks);
		}
	}
}
