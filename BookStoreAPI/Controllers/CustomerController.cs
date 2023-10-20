using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;

namespace BookStoreAPI.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository = new CustomerRepository();
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetCustomers() => repository.GetCustomers();

        //[HttpGet("{id}")]
        //public ActionResult<AppUser> GetUserById(string id)
        //{
        //    var user = repository.GetUserById(id);
        //    if (user == null)
        //        return NotFound();
        //    return Ok(user);
        //}

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] AppUser user)
        //{
        //    return Ok(repository.SaveUser(user));
        //}


        //[HttpDelete("{id}")]
        //public IActionResult DeleteUsers(string id)
        //{
        //    var user = repository.GetUserById(id);
        //    if (user == null)
        //        return NotFound();
        //    repository.DeleteUserById(user);
        //    return Ok();
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateUser(string id, AppUser user)
        //{
        //    var existingUser = repository.GetUserById(id);
        //    if (existingUser == null)
        //        return NotFound();
        //    user.Id = id;
        //    return Ok(repository.UpdateUser(user));
        //}
    }
}
