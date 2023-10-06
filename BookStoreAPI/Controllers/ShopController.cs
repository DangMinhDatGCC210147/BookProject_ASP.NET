using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using DataAccess;

namespace BookStoreAPI.Controllers
{
    [Route("api/Shops")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly IShopRepository repository = new ShopRepository();

        [HttpGet]
        public ActionResult<Filter> GetFilter()
        {
            Filter filters = new Filter();
            filters.genres = repository.GenreAndQuantity();
            filters.publishers = repository.PublisherAndQuantity();
            filters.languages = repository.LanguageAndQuantity();
            filters.authors = repository.AuthorAndQuantity();
            
            return filters;
        }
    }
}
