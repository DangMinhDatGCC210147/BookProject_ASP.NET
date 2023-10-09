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
    public class LanguagesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string LanguageApiUrl = "";

        public LanguagesController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LanguageApiUrl = "/api/Languages";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(LanguageApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Language> languages = JsonSerializer.Deserialize<List<Language>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(languages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language p)
        {
            HttpResponseMessage response = await client.GetAsync(LanguageApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Language> languages = JsonSerializer.Deserialize<List<Language>>(strData, options);
            return View(languages);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Language language)
        {
            language.Id = id;
            string data = JsonSerializer.Serialize(language);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(LanguageApiUrl + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Language");
            }
            return View(language);
        }
    }
}
