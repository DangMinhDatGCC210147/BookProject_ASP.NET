using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreAPI.Controllers
{
	[Route("api/Users")]
	[ApiController]
	public class UserController : ControllerBase
	{
        private readonly IUserRepository repository = new UserRepository();
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers() => repository.GetUsers();

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUserById(string id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] AppUser user)
        {
            return Ok(repository.SaveUser(user));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(string id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
                return NotFound();
            repository.DeleteUserById(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, AppUser user)
        {
            var existingUser = repository.GetUserById(id);
            if (existingUser == null)
                return NotFound();
            user.Id = id; // Make sure the ID is set to the correct value
            return Ok(repository.UpdateUser(user));
        }
    }
}
