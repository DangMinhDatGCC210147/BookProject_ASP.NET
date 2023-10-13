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
    public class PublishersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string PublisherApiUrl = "";

        public PublishersController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = "/api/Publishers";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(PublisherApiUrl);

            string data = await httpResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(data, options);

            return View(publishers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher p)
        {
            HttpResponseMessage response = await client.GetAsync(PublisherApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            List<Publisher> publishers = JsonSerializer.Deserialize<List<Publisher>>(strData, options);
            return View(publishers);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            publisher.Id = id;
            string data = JsonSerializer.Serialize(publisher);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(PublisherApiUrl + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Publishers");
            }
            return View(publisher);
        }
    }
}
