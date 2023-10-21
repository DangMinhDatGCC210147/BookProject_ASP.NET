using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace BookStoreAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository = new UserRepository();

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

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] AppUser user)
        //{
        //    return Ok(repository.SaveUser(user));
        //}


        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(string id)
        {
            var user = repository.GetUserById(id);
            if (user == null)
                return NotFound();
            repository.DeleteUserById(user);
            return Ok();
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser(string id, AppUser user)
        //{
        //    var existingUser = repository.GetUserById(id);
        //    if (existingUser == null)
        //        return NotFound();
        //    user.Id = id; 
        //    return Ok(repository.UpdateUser(user));
        //}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id, [FromBody] string newPassword)
        {
            var existingUser = repository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Kiểm tra xem có cần cập nhật mật khẩu không
            if (!string.IsNullOrEmpty(newPassword))
            {
                // Sử dụng PasswordHasher để hash mật khẩu mới
                var passwordHasher = new PasswordHasher<AppUser>();
                existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, newPassword);

                // Gọi phương thức cập nhật từ repository hoặc DbContext của bạn để chỉ cập nhật mật khẩu
                var updatedUser = repository.UpdateUser(existingUser);

                if (updatedUser == null)
                {
                    return BadRequest("Failed to update password.");
                }

                return Ok(updatedUser);
            }
            else
            {
                return BadRequest("New password is required. Check again in UserController");
            }
        }

    }
}
