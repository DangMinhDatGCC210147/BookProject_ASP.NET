using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Areas.Owner.Controllers
{
    [Authorize(Roles = "StoreOwner")]
    [Area("Owner")]
    public class OrdersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string OrderApiUrl = "";

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "/api/Orders";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(OrderApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order p)
        {
            HttpResponseMessage response = await client.GetAsync(OrderApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            return View(orders);
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(id);
        }
    }
}
