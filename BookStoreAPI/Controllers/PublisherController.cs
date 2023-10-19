using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using OfficeOpenXml;

namespace BookStoreAPI.Controllers
{
    [Route("api/Publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository repository = new PublisherRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> GetPublishers() => repository.GetPublishers();

        [HttpGet("{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)
        {
            var publisher = repository.GetPublisherById(id);
            if (publisher == null)
                return NotFound();

            return Ok(publisher);
        }

        [HttpPost]

        public IActionResult CreatePublisher(Publisher publisher)
        {
            repository.SavePublisher(publisher);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublishers(int id)
        {
            var publisher = repository.GetPublisherById(id);
            if (publisher == null)
                return NotFound();
            repository.DeletePublisherById(publisher);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, Publisher publisher)
        {
            var existingPublisher = repository.GetPublisherById(id);
            if (existingPublisher == null)
                return NotFound();

            publisher.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdatePublisher(publisher));
        }


        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetPublishers().ToList();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Publisher"); 
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
