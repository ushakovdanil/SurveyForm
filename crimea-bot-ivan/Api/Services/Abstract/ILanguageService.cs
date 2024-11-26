using Api.Models.Entities;

namespace Api.Services.Abstract
{
    public interface ILanguageService
    {
        Task<Language> GetLanguageById(Guid id);
        Task<Language> GetLanguageByName(string name);
        void AddLanguage(Language language);
        void UpdateLanguage(Language language);
        void DeleteLanguage(Language language);
    }
}
