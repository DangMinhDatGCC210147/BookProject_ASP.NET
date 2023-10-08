using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        void SaveLanguage(Language p);
		Language GetLanguageById(int id);
        void DeleteLanguageById(Language p);
        void UpdateLanguage(Language p);
        List<Language> GetLanguages();
    }
}