using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Controllers
{
	[Route("api/Profile")]
	[ApiController]
	public class ProfileController : ControllerBase
	{
        private readonly IUserRepository repository = new UserRepository();
        
        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUserById(string id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(string userId, AppUser user)
        {
            var existingUser = repository.GetUserById(userId);
            if (existingUser == null)
                return NotFound();
            existingUser.PasswordHash = user.PasswordHash;
            return Ok(repository.UpdateUser(existingUser));
        }
    }
}
