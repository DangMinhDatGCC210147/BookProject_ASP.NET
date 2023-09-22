using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace BookStoreAPI.Controllers
{
	[Route("api/Laguages")]
	[ApiController]
	public class LanguageController : ControllerBase
	{
		private readonly ILanguageRepository repository;

		public LanguageController(ILanguageRepository languageRepository)
		{
			repository = languageRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Language>> GetLanguages()
		{
			var languages = repository.GetLanguages();
			return Ok(languages);
		}

		[HttpGet("{id}")]
		public ActionResult<Language> GetLanguageById(int id)
		{
			var language = repository.GetLanguageById(id);
			if (language == null)
				return NotFound();

			return Ok(language);
		}

		[HttpPost]
		public IActionResult CreateLanguage(Language language)
		{
			repository.SaveLanguage(language);
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
