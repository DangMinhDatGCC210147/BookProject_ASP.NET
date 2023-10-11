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
    public class ReviewsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string ReviewApiUrl = "";

        public ReviewsController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ReviewApiUrl = "/api/Reviews";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(ReviewApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Review> reviews = JsonSerializer.Deserialize<List<Review>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review p)
        {
            HttpResponseMessage response = await client.GetAsync(ReviewApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Review> reviews = JsonSerializer.Deserialize<List<Review>>(strData, options);
            return View(reviews);
        }

        public ActionResult Detail()
        {
            return View();
        }

        //public ActionResult Edit(int id)
        //{
        //    return View(id);
        //}
    }
}
