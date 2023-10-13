using BusinessObjects;
using BusinessObjects.Data.Enum;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Areas.Owner.Controllers
{
    [Authorize(Roles = "StoreOwner")]
    [Area("Owner")]
    public class AuthorsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string AuthorApiUrl = "";

        public AuthorsController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "/api/Authors";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(AuthorApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(authors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author p)
        {
            HttpResponseMessage response = await client.GetAsync(AuthorApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Author> authors = JsonSerializer.Deserialize<List<Author>>(strData, options);
            return View(authors);
        }

        public ActionResult Detail()
        {
            return View();
        }

        public async Task<ActionResult> EditAsync(int id, Author author)
        {
            author.Id = id;
            string data = JsonSerializer.Serialize(author);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(AuthorApiUrl + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Author");
            }
            return View(author);
        }
    }
}
