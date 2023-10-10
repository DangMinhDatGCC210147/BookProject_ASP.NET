using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/Languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository repository = new LanguageRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages() => repository.GetLanguages();

        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguageById(int id)
        {
            var language = repository.GetLanguageById(id);
            if (language == null)
                return NotFound();

            return Ok(language);
        }

        [HttpPost]
        public IActionResult CreateLanguage([FromBody] Language language)
        {
            return Ok(repository.SaveLanguage(language));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLanguages(int id)
        {
            var product = repository.GetLanguageById(id);
            if (product == null)
                return NotFound();
            repository.DeleteLanguageById(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, Language language)
        {
            var existingLanguage = repository.GetLanguageById(id);
            if (existingLanguage == null)
                return NotFound();

            language.Id = id; 
            return Ok(repository.UpdateLanguage(language));
        }
    }
}
