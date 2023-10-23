using BookStore.Models;
using BusinessObjects;
using BusinessObjects.Data.Enum;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStoreWebClient.Controllers
{
	[Authorize(Roles = "Customer")]
	public class PaymentController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly HttpClient client = null;
		private string cartApiUrl = "";
		private string CartDetailApiUrl = "";
		private string orderApiUrl = "";
		private string orderDetaiApilUrl = "";
		private string bookApilUrl = "";

		public PaymentController(IConfiguration configuration, UserManager<AppUser> userManager)
		{
			_userManager = userManager;
			_configuration = configuration;
			client = new HttpClient();
			client.BaseAddress = new Uri(_configuration["BaseAddress"]);
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			cartApiUrl = "/api/Carts";
			CartDetailApiUrl = "/api/CartDetails";
			orderApiUrl = "/api/Orders";
			orderDetaiApilUrl = "/api/OrderDetails";
			bookApilUrl = "/api/Products";
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Save([FromForm] UserPayment userPayment)
		{
			AppUser getuser = await _userManager.GetUserAsync(User);
			
			// set to Object form form
			Order order = new Order();
			order.DeliveryDate = DateTime.Now;
			order.DeleveryLocal = userPayment.Address + userPayment.SubAddress;
			order.UserId = getuser.Id;
			order.CustomerName = userPayment.FirstName + " " + userPayment.LastName;
			order.CustomerPhone = userPayment.PhoneNumber;
			order.Total = userPayment.Total;   
			order.IsConfirm = false;
			if (userPayment.DiscountId != 0)
			{
				order.DiscountId = userPayment.DiscountId;
			}            

			// save into Order
			string order_data = JsonSerializer.Serialize(order);
			var content_order = new StringContent(order_data, System.Text.Encoding.UTF8, "application/json");

			HttpResponseMessage responseOrder = await client.PostAsync(orderApiUrl, content_order);
			if (responseOrder.IsSuccessStatusCode)
			{
				// Retrieve order id inserted
				var data = responseOrder.Content.ReadAsStringAsync().Result;
				var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				var obj = System.Text.Json.JsonSerializer.Deserialize<Order>(data, options);
				int orderId = obj.Id;

				// Get cart By userId
				HttpResponseMessage cart_Response = await client.GetAsync(CartDetailApiUrl + "/" + getuser.Id);
				string cart_data = await cart_Response.Content.ReadAsStringAsync();
				var cart_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
				List<BookCart> cartDetail = JsonSerializer.Deserialize<List<BookCart>>(cart_data, cart_options);

				List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
				// Get book 
				if (cartDetail != null)
				{
					foreach (var item in cartDetail)
					{
						OrderDetail orderDetail = new OrderDetail
						{
							OrderId = orderId,
							BookId = item.BookId,
							Quantity = item.Quantity,
							UnitPrice = item.Price
						};
						lstOrderDetail.Add(orderDetail);
					}
				}

				// save into OrderDetail
				foreach (var item in lstOrderDetail)
                {
					string orderDetail_data = JsonSerializer.Serialize(item);
					var content_orderDetail = new StringContent(orderDetail_data, System.Text.Encoding.UTF8, "application/json");
					HttpResponseMessage orderDetail_response = await client.PostAsync(orderDetaiApilUrl, content_orderDetail);

					// Update Quantity in Stock
					BookQuantity bookQuantity = new BookQuantity();	/// create obj
					bookQuantity.BookId = item.BookId;
					bookQuantity.SoldQuantity = item.Quantity;

					string book_data = JsonSerializer.Serialize(bookQuantity);	
					var book_content = new StringContent(book_data, System.Text.Encoding.UTF8, "application/json");
					HttpResponseMessage update_response = await client.PutAsync(bookApilUrl + "/Quantity/Update", book_content);

					// Clear Cart
					HttpResponseMessage httpResponse = await client.DeleteAsync(CartDetailApiUrl + "/ClearCart/" + getuser.Id);	
				}

				return RedirectToAction("Cart", "Home");
			}

			return RedirectToAction("CheckOut", "Home", userPayment.DiscountId);
		}
	}
}
