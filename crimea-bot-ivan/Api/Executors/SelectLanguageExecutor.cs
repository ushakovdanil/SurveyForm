using Api.Mappers;
using Api.Models.Entities;
using Api.Services;
using Api.Services.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Api.Executors
{
    public class SelectLanguageExecutor : NavigationThemeExecutor, IExecutor
    {
        public SelectLanguageExecutor(UserEntity userEntity, 
            string message,
            IBotThemeService botThemeService,
            ITelegramBotClient botClient,
            IUserService userService,
            ILanguageService languageService,
            IRequestService requestService) : base(userEntity, message, botThemeService, botClient, userService, languageService, requestService) {}
        public override async Task Execute()
        {
            Language selectedLanguage = await GetLanguageByButtonValue(Message);
            if (selectedLanguage != null) 
            {
                UserEntity.Language = selectedLanguage;
                UserService.UpdateUser(UserEntity);
            }
            await base.Execute();
        }
        private async Task<Language> GetLanguageByButtonValue(string languageName)
        {
            var theme = await BotThemeService.GetLocalizedThemeById(UserEntity.LastTheme.Id, UserEntity.Language);
            var selectedLanguage = theme.BotMessages.SelectMany(x => x.ReplyKeyboardMarkup)
                    .Where(t => t.LocalizedTexts.Any(l => l.Value == languageName))
                    .Select(b => b.CallbackValue)
                    .FirstOrDefault();
            return await LanguageService.GetLanguageByName(selectedLanguage);
        }
    }
}
