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
        public IActionResult CreateLanguage([FromForm] Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                repository.SaveLanguage(language);
                //return Ok();
                return RedirectToAction("Index","Languages", new { area = "Owner"});
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
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

            language.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateLanguage(language);
            return Ok();
        }
    }
}
