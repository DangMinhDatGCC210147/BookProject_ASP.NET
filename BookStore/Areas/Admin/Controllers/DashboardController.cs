using BookStoreAPI.Models;
using BusinessObjects;
using BusinessObjects.Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string StatisticApiUrl = "";

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StatisticApiUrl = "/api/Statistics";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;
            HttpResponseMessage httpResponse = await client.GetAsync(StatisticApiUrl + "?currentDate=" + now); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            StatisticView statistics = JsonSerializer.Deserialize<StatisticView>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(statistics);
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Book()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult Language()
        {
            return View();
        }
        public IActionResult Feedback()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Author()
        {
            return View();
        }

        public IActionResult Publisher()
        {
            return View();
        }
        public IActionResult Discount()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
