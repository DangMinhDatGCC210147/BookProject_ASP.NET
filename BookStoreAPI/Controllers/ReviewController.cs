using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

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
    }
}
