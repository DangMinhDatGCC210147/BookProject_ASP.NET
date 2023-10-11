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
    public class FeedbacksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string FeedbackApiUrl = "";

        public FeedbacksController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            FeedbackApiUrl = "/api/Feedbacks"; // Adjust the API endpoint for Feedbacks
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(FeedbackApiUrl);

            string data = await httpResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<FeedBack> feedbacks = JsonSerializer.Deserialize<List<FeedBack>>(data, options);

            return View(feedbacks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedBack feedback) // Adjust the parameter type to Feedback
        {
            string data = JsonSerializer.Serialize(feedback);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(FeedbackApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feedbacks"); // Adjust the redirect action and controller
            }

            return View(feedback);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FeedBack feedback) // Adjust the parameter type to Feedback
        {
            feedback.Id = id;
            string data = JsonSerializer.Serialize(feedback);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(FeedbackApiUrl + "/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feedbacks"); // Adjust the redirect action and controller
            }

            return View(feedback);
        }
    }
}
