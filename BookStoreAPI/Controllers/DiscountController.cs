using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using System.Collections.Generic;
using OfficeOpenXml;

namespace BookStoreAPI.Controllers
{
    [Route("api/Discounts")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository repository = new DiscountRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Discount>> GetDiscounts() => repository.GetDiscounts();

        [HttpGet("{id}")]
        public ActionResult<Discount> GetDiscountById(int id)
        {
            var discount = repository.GetDiscountById(id);
            if (discount == null)
                return NotFound();

            return Ok(discount);
        }

        [HttpPost]
        public IActionResult CreateDiscount([FromBody] Discount discount)
        {
            return Ok(repository.SaveDiscount(discount));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var discount = repository.GetDiscountById(id);
            if (discount == null)
                return NotFound();
            repository.DeleteDiscountById(discount);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDiscount(int id, Discount discount)
        {
            var existingDiscount = repository.GetDiscountById(id);
            if (existingDiscount == null)
                return NotFound();

            discount.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdateDiscount(discount));
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetDiscounts().ToList();
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
