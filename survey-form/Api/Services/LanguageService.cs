using Api.Models.Entities;
using Api.Repository.Abstract;
using Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class LanguageService : ILanguageService
    {
        private IRepository<Language> _languageRepository;
        public LanguageService(IRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }
        public Task<Language> GetLanguageById(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<Language> GetLanguageByName(string name)
        {
            return await _languageRepository.FilteredList(language => language.Name == name).SingleOrDefaultAsync();
        }
        public void UpdateLanguage(Language language)
        {
            throw new NotImplementedException();
        }
        public void AddLanguage(Language language)
        {
            throw new NotImplementedException();
        }
        public void DeleteLanguage(Language language)
        {
            throw new NotImplementedException();
        }

    }
}
