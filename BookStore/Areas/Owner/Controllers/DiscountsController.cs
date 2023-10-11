using BusinessObjects;
using BusinessObjects.Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Areas.Owner.Controllers
{
    [Authorize(Roles = "StoreOwner")]
    [Area("Owner")]
    public class DiscountsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string DiscountApiUrl = "";

        public DiscountsController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DiscountApiUrl = "/api/Discounts"; // Adjust the API endpoint for discounts
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(DiscountApiUrl);

            string data = await httpResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Discount> discounts = JsonSerializer.Deserialize<List<Discount>>(data, options);

            return View(discounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discount p)
        {
            HttpResponseMessage response = await client.GetAsync(DiscountApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            List<Discount> discounts = JsonSerializer.Deserialize<List<Discount>>(strData, options);
            return View(discounts);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Discount discount)
        {
            discount.Id = id;
            string data = JsonSerializer.Serialize(discount);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(DiscountApiUrl + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Discount");
            }
            return View(discount);
        }
    }
}
