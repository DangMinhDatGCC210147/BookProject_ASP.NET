using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;


namespace BookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BookController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "/api/Products";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(ProductApiUrl); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<Product> clubs = JsonSerializer.Deserialize<List<Product>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(clubs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(products);
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
