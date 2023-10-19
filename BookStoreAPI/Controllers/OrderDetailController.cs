using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/OrderDetails")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository repository = new OrderDetailRepository();

        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetails() => repository.GetOrderDetails();

        [HttpGet()]
        public ActionResult<OrderDetail> GetOrderDetailById(OrderDetail orderDetail)
        {
            var OrderDetail = repository.GetOrderDetailById(orderDetail.BookId, orderDetail.OrderId);
            if (OrderDetail == null)
                return NotFound();

            return Ok(OrderDetail);
        }

        [HttpPost]
        public IActionResult CreateOrderDetail([FromBody] OrderDetail OrderDetail)
        {
            return Ok(repository.SaveOrderDetail(OrderDetail));
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetOrderDetails().ToList();
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
