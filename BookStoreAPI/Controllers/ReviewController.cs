using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using OfficeOpenXml;

namespace BookStoreAPI.Controllers
{
    [Route("api/Reviews")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository repository = new ReviewRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetReviews() => repository.GetReviews();

        [HttpGet("{id}")]
        public ActionResult<Review> GetReviewById(int id)
        {
            var review = repository.GetReviewById(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public IActionResult CreateReview(Review review)
        {
            return Ok(repository.SaveReview(review));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReviews(int id)
        {
            var review = repository.GetReviewById(id);
            if (review == null)
                return NotFound();
            repository.DeleteReviewById(review);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, Review review)
        {
            var existingReview = repository.GetReviewById(id);
            if (existingReview == null)
                return NotFound();

            review.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdateReview(review));
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetReviews().ToList();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Review"); 
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
