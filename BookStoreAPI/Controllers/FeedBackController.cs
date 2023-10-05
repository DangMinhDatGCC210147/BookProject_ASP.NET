using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

namespace BookStoreAPI.Controllers
{
    [Route("api/Feedbacks")]
    [ApiController]
    public class FeedBackController : Controller
    {
        private readonly IFeedBackRepository repository = new FeedbackRepository();

        [HttpGet]
        public ActionResult<IEnumerable<FeedBack>> GetFeedBacks() => repository.GetFeedBacks();

        [HttpGet("{id}")]
        public ActionResult<FeedBack> GetFeedBackById(int id)
        {
            var feedBack = repository.GetFeedBackById(id);
            if (feedBack == null)
                return NotFound();

            return Ok(feedBack);
        }

        [HttpPost]
        public IActionResult CreateFeedBack(FeedBack feedBack)
        {
            repository.SaveFeedBack(feedBack);
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteFeedBacks(int id)
        {
            var feedBack = repository.GetFeedBackById(id);
            if (feedBack == null)
                return NotFound();
            repository.DeleteFeedBackById(feedBack);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFeedBack(int id, FeedBack feedBack)
        {
            var existingFeedBack = repository.GetFeedBackById(id);
            if (existingFeedBack == null)
                return NotFound();

            feedBack.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateFeedBack(feedBack);
            return Ok();
        }
    }
}
