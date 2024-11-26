using Api.Models.Entities;
using Api.Models.Enums;
using Api.Services.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Telegram.Bot;

namespace Api.Executors
{
    public class UserBoolRequestExecutor : UserRequestExecutor
    {
        public UserBoolRequestExecutor(UserEntity userEntity, 
            string message, 
            IBotThemeService botThemeService, 
            ITelegramBotClient botClient, 
            IUserService userService, 
            ILanguageService languageService, 
            IRequestService requestService) : base(userEntity, message, botThemeService, botClient, userService, languageService, requestService)
        {
        }

        public override async Task Execute()
        {
            Request request = await RequestService.GetInProgressRequestByUser(UserEntity);
            if (request == null)
            {
                request = new Request()
                {
                    User = UserEntity,
                    Status = UserRequestStatus.InProgres
                };

                RequestService.AddRequest(request);
            }
            var currentUserThemeId = UserEntity.LastTheme.Id;
            var currentUserLanguage = UserEntity.Language;
            var nextTheme = await BotThemeService.GetLocalizedThemeToNavigate(currentUserThemeId, Message, currentUserLanguage);
            if (nextTheme != null)
            {
                string propertyName = UserEntity.LastTheme.Name;

                var result = await GetCallbackByButtonValue(Message);

                switch (result)
                {
                    case "true":
                        request[propertyName] = true;
                        break;
                    case "false":
                        request[propertyName] = false;
                        break;
                    default:
                        break;
                }
                RequestService.UpdateRequest(request);

                await SendNextStepToUser(nextTheme, request);
            }
            else
            {

                await SendThemeToUser(UserEntity.LastTheme, BotMessageType.Error);
            }
        }

        protected async Task<string> GetCallbackByButtonValue(string selectedButtonText)
        {
            var theme = await BotThemeService.GetLocalizedThemeById(UserEntity.LastTheme.Id, UserEntity.Language);
            var callback = theme.BotMessages.SelectMany(x => x.ReplyKeyboardMarkup)
                    .Where(t => t.LocalizedTexts.Any(l => l.Value == selectedButtonText))
                    .Select(b => b.CallbackValue)
                    .FirstOrDefault();
            return callback;
        }
    }
}
