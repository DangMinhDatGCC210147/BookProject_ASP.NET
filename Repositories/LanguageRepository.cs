using BusinessObjects;
using DatAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class LanguageRepository : ILanguageRepository
	{
		public void DeleteLanguageById(Language language) => LanguageDAO.DeleteLanguage(language);

		public Language GetLanguageById(int id) => LanguageDAO.FindLanguageById(id);

		public List<Language> GetLanguages() => LanguageDAO.GetLanguages();

		public Language SaveLanguage(Language language) => LanguageDAO.SaveLanguage(language);

		public Language UpdateLanguage(Language language) => LanguageDAO.UpdateLanguage(language);

	}
}
