using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Areas.Admin.Controllers
{        
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient client = null;
        private string CustomerApi = "";

        private readonly ICustomerRepository repository = new CustomerRepository();

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CustomerApi = "/api/Customers";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage httpResponse = await client.GetAsync(CustomerApi); //gửi một yêu cầu HTTP GET đến một đường dẫn API được truyền vào qua biến api. 

            string data = await httpResponse.Content.ReadAsStringAsync();//phản hồi của API, thường là chuỗi JSON

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //phân tích cú pháp JSON không phân biệt hoa/thường của tên thuộc tính

            List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(data, options);//truy vấn tất cả các bản ghi trong bảng Clubs trong csdl và lưu kq vào biến club dưới dạng một danh sách (List).

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser p)
        {
            HttpResponseMessage response = await client.GetAsync(CustomerApi);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(strData, options);
            return View(users);
        }

        [HttpPost]
        public ActionResult Detail()
        {
            return View();
        }

        public async Task<ActionResult> EditAsync(string id, AppUser user)
        {
            user.Id = id;
            string data = JsonSerializer.Serialize(user);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(CustomerApi + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }
        public ActionResult<IEnumerable<AppUser>> GetAccount()
        {
            // Kết nối đến cơ sở dữ liệu và truy vấn dữ liệu
            var data = repository.GetCustomers().ToList();

            // Truyền dữ liệu đến View
            ViewBag.MyData = data;

            return View();
        }
    }
}
