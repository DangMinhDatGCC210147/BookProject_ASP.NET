using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using OfficeOpenXml;

namespace BookStoreAPI.Controllers
{
    [Route("api/Feedbacks")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IFeedBackRepository repository = new FeedbackRepository();

        [HttpGet]
        public ActionResult<IEnumerable<FeedBack>> GetFeedBacks() => repository.GetFeedbacks();

        [HttpGet("{id}")]
        public ActionResult<FeedBack> GetFeedBackById(int id)
        {
            var feedBack = repository.GetFeedbackById(id);
            if (feedBack == null)
                return NotFound();

            return Ok(feedBack);
        }

        [HttpPost]
        public IActionResult CreateFeedBack(FeedBack feedBack)
        {
           return Ok(repository.SaveFeedback(feedBack));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedBacks(int id)
        {
            var feedBack = repository.GetFeedbackById(id);
            if (feedBack == null)
                return NotFound();
            repository.DeleteFeedbackById(feedBack);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFeedBack(int id, FeedBack feedBack)
        {
            var existingFeedBack = repository.GetFeedbackById(id);
            if (existingFeedBack == null)
                return NotFound();

            feedBack.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdateFeedback(feedBack));
        }

        //////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetFeedbacks().ToList();
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
