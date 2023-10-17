using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository repository = new OrderRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => repository.GetOrders();

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = repository.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            return Ok(repository.SaveOrder(order));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = repository.GetOrderById(id);
            if (order == null)
                return NotFound();
            repository.DeleteOrderById(order);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var existingOrder = repository.GetOrderById(id);
            if (existingOrder == null)
                return NotFound();

            order.Id = id;
            return Ok(repository.UpdateOrder(order));
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetOrders().ToList();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
