using Api.Models.Entities;
using Api.Repository.Abstract;
using Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
    public class BotThemeService : IBotThemeService
    {
        private IRepository<BotThemeEntity> _botThemeRepository;

        public BotThemeService(IRepository<BotThemeEntity> botThemeRepository)
        {
            _botThemeRepository = botThemeRepository;
        }

        public async Task<BotThemeEntity> GetLocalizedThemeToNavigate(Guid currentThemeId, string button, Language language)
        {
            var result = await _botThemeRepository.FilteredList(x => x.Id == currentThemeId)
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.BotThemeToNavigate)
                        .ThenInclude(bt => bt.BotMessages)
                            .ThenInclude(btr => btr.ReplyKeyboardMarkup)
                                .ThenInclude(btrt => btrt.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.BotThemeToNavigate)
                        .ThenInclude(bt => bt.BotMessages)
                            .ThenInclude(btrt => btrt.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(m => m.LocalizedTexts.Where(l => l.Language == language))
                .SelectMany(x => x.BotMessages)
                    .Where(b => b.ReplyKeyboardMarkup.Any(t => t.LocalizedTexts.Any(l => l.Value == button)))
                    .SelectMany(b => b.ReplyKeyboardMarkup)
                    .Where(b => b.LocalizedTexts.Any(l => l.Value == button))
                    .Select(b => b.BotThemeToNavigate)
                    .FirstOrDefaultAsync();
            
            return result;
        }

        public async Task<BotThemeEntity> GetLocalizedChildThemeByName(Guid currentThemeId, string name, Language language)
        {
            var result = await _botThemeRepository.FilteredList(x => x.Name == name && x.ParentTheme.Id == currentThemeId)
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.LocalizedTexts.Where(b => b.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.LocalizedTexts.Where(l => l.Language == language))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<BotThemeEntity> GetLocalizedChildThemeByParentThemeId(Guid currentThemeId, Language language)
        {
            var result = await _botThemeRepository.FilteredList(x => x.ParentTheme.Id == currentThemeId)
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.LocalizedTexts.Where(b => b.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.LocalizedTexts.Where(l => l.Language == language))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<BotThemeEntity> GetLocalizedThemeByName(string name, Language language)
        {
            var result = await _botThemeRepository.FilteredList(x => x.Name == name)
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.BotThemeToNavigate)
                        .ThenInclude(bt => bt.BotMessages)
                            .ThenInclude(btr => btr.ReplyKeyboardMarkup)
                                .ThenInclude(btrt => btrt.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.BotThemeToNavigate)
                        .ThenInclude(bt => bt.BotMessages)
                            .ThenInclude(btrt => btrt.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.LocalizedTexts.Where(l => l.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(m => m.LocalizedTexts.Where(l => l.Language == language))
                .SingleOrDefaultAsync();

            return result;
        }
        public async Task<BotThemeEntity> GetLocalizedThemeById(Guid id, Language language)
        {
            var result = await _botThemeRepository.FilteredList(x => x.Id == id)
                .Include(x => x.BotMessages)
                .ThenInclude(b => b.LocalizedTexts.Where(b => b.Language == language))
                .Include(x => x.BotMessages)
                    .ThenInclude(b => b.ReplyKeyboardMarkup)
                    .ThenInclude(t => t.LocalizedTexts.Where(l => l.Language == language))
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<string>> GetLocalizedThemeMessages(BotThemeEntity botThemeEntity)
        {
            List<string> texts = new List<string>();
            foreach (var botMessage in botThemeEntity.BotMessages)
            {
                texts.Add(botMessage.LocalizedTexts.FirstOrDefault().Value);
            }

            return texts;
        }
    }
}
