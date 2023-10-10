using BookStore.Models;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Language SaveLanguage(Language p);
		List<Language> GetLanguages();
		Language GetLanguageById(int id);
        void DeleteLanguageById(Language p);
        Language UpdateLanguage(Language p);    
    }
}