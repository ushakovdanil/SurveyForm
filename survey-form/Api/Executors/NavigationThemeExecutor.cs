using Api.Mappers;
using Api.Models.Entities;
using Api.Models.Enums;
using Api.Services.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Api.Executors
{
    public class NavigationThemeExecutor : INavigationThemeExecutor, IExecutor
    {
        protected string Message;
        protected UserEntity UserEntity;
        protected IBotThemeService BotThemeService;
        protected ITelegramBotClient BotClient;
        protected IUserService UserService;
        protected ILanguageService LanguageService;
        protected IRequestService RequestService;
        public NavigationThemeExecutor(UserEntity userEntity,
            string message,
            IBotThemeService botThemeService,
            ITelegramBotClient botClient,
            IUserService userService,
            ILanguageService languageService,
            IRequestService requestService)
        {
            UserEntity = userEntity;
            Message = message;
            BotThemeService = botThemeService;
            BotClient = botClient;
            UserService = userService;
            LanguageService = languageService;
            RequestService = requestService;
        }
        public virtual async Task Execute()
        {
            var currentUserThemeId = UserEntity.LastTheme.Id;
            var currentUserLanguage = UserEntity.Language;
            var nextTheme = await BotThemeService.GetLocalizedThemeToNavigate(currentUserThemeId, Message, currentUserLanguage);
            if (nextTheme == null)
            {
                nextTheme = await BotThemeService.GetLocalizedThemeById(currentUserThemeId, currentUserLanguage);
            }
            await SendThemeToUser(nextTheme);
        }

        public async Task SendThemeToUser(BotThemeEntity theme)
        {
            var messages = theme.BotMessages.Where(x => x.BotMessageType == BotMessageType.Regular);

            foreach (var message in messages)
            {
                ReplyKeyboardMarkup? replyKeyaboard = null;
                if (message.ReplyKeyboardMarkup.Count > 0)
                {
                    replyKeyaboard = message.ReplyKeyboardMarkup.ToDomain();
                }

                await BotClient.SendTextMessageAsync(
                    chatId: UserEntity.TelegramId,
                    text: message.LocalizedTexts.FirstOrDefault()!.Value,
                    replyMarkup: replyKeyaboard);

                if (message.ReplyKeyboardMarkup.Count > 0)
                {
                    UserEntity.LastTheme = theme;
                    UserService.UpdateUser(UserEntity);
                }
            }
        }

        public async Task SendThemeToUser(BotThemeEntity theme, BotMessageType botMessageType)
        {
            var messages = theme.BotMessages.Where(x => x.BotMessageType == botMessageType);

            if(!messages.Any())
            {
                return;
            }

            foreach (var message in messages)
            {
                ReplyKeyboardMarkup? replyKeyaboard = null;
                if (message.ReplyKeyboardMarkup.Count > 0)
                {
                    replyKeyaboard = message.ReplyKeyboardMarkup.ToDomain();
                }

                await BotClient.SendTextMessageAsync(
                    chatId: UserEntity.TelegramId,
                    text: message.LocalizedTexts.FirstOrDefault()!.Value,
                    replyMarkup: replyKeyaboard);

                if (message.ReplyKeyboardMarkup.Count > 0)
                {
                    UserEntity.LastTheme = theme;
                    UserService.UpdateUser(UserEntity);
                }
            }
        }
    }
}
