using Api.Models.Entities;
using Api.Models.Enums;
using Api.Services.Abstract;
using Api.Validators.Abstract;
using Telegram.Bot;

namespace Api.Executors
{
    public class UserRequestExecutor : NavigationThemeExecutor, IExecutor
    {
        public UserRequestExecutor(UserEntity userEntity, 
            string message, 
            IBotThemeService botThemeService, 
            ITelegramBotClient botClient, 
            IUserService userService, 
            ILanguageService languageService,
            IRequestService requestService) : base(userEntity, message, botThemeService, botClient, userService, languageService, requestService){}

        public override async Task Execute()
        {
            Request request = await RequestService.GetInProgressRequestByUser(UserEntity);
            if(request == null)
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
                await SendThemeToUser(nextTheme);
            }
            else
            {
                string propertyName = UserEntity.LastTheme.Name;

                if(!await ValidateProperty())
                {
                    return;
                }

                request[propertyName] = Message.ToString();
                RequestService.UpdateRequest(request);

                nextTheme = await BotThemeService.GetLocalizedChildThemeByParentThemeId(currentUserThemeId, currentUserLanguage);

                await SendNextStepToUser(nextTheme, request);
            }
        }

        protected async Task SendNextStepToUser(BotThemeEntity nextTheme, Request request)
        {
            if (nextTheme == null)
            {
                request.CalculateRating();
                RequestService.UpdateRequest(request);

                var finalMessages = UserEntity.LastTheme.BotMessages.Where(x => x.BotMessageType == BotMessageType.Final).ToList();

                if (finalMessages.Any())
                {
                    foreach (var finalMessage in finalMessages)
                    {
                        var text = finalMessage.LocalizedTexts.FirstOrDefault()!.Value;
                        await BotClient.SendTextMessageAsync(chatId: UserEntity.TelegramId, text: text);
                    }
                }

                nextTheme = await BotThemeService.GetLocalizedThemeByName("Main", UserEntity.Language);
            }

            await SendThemeToUser(nextTheme);
        }

        protected async Task<bool> ValidateProperty()
        {
            Type executorType = Type.GetType("Api.Validators." + UserEntity.LastTheme.Validator);
            if (executorType != null)
            {
                IValidator<string> validator = (IValidator<string>)Activator.CreateInstance(executorType)!;

                if (!validator.Validate(Message))
                {
                    await SendThemeToUser(UserEntity.LastTheme, BotMessageType.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
