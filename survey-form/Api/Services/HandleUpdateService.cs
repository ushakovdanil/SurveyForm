using Api.Models.Entities;
using Api.Services.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Api.Services
{
    public class HandleUpdateService : IHandleUpdateService
    {
        private ILogger<HandleUpdateService> _logger;
        private IUserService _userService;
        private IBotThemeService _botThemeService;
        private ILanguageService _languageService;
        private ITelegramBotClient _botClient;
        private IRequestService _requestService;

        public HandleUpdateService(
        ILogger<HandleUpdateService> logger,
        IUserService userService,
        IBotThemeService botThemeService,
        ILanguageService languageService,
        ITelegramBotClient bot,
        IRequestService requestService)
        {
            _botClient = bot;
            _logger = logger;
            _userService = userService;
            _languageService = languageService;
            _botThemeService = botThemeService;
            _requestService = requestService;
        }
        public async Task HandleUpdateAsync(Update update)
        {
            try
            {
                if (update.Message == null || update.Message.Chat.Id<0)
                {
                    return;
                }

                BotThemeEntity theme;
                UserEntity user;

                if (await _userService.IsUserByTelegramIdExist(update.Message.Chat.Id) == false)
                {
                    var defaultLanguage = await _languageService.GetLanguageByName("Ukrainian");
                    Console.WriteLine(defaultLanguage.Id);
                    var defaultTheme = await _botThemeService.GetLocalizedThemeByName("First Select Language", defaultLanguage);
                    Console.WriteLine(defaultTheme.Id);
                    user = new UserEntity(
                        update.Message.Chat.Id,
                        update.Message.Chat.Username,
                        defaultTheme,
                        defaultLanguage
                        );
                    Console.WriteLine(user.Id);
                    _userService.AddUser(user);
                    await ExecuteExecutor(defaultTheme, user, update.Message.Text);
                    return;
                }
                else if(update.Message.Text == "/start")
                {
                    user = await _userService.GetUserByTelegramId(update.Message.Chat.Id);
                    theme = await _botThemeService.GetLocalizedThemeByName("Main", user.Language);
                    user.LastTheme = theme;
                    _userService.UpdateUser(user);
                }
                else
                {
                    user = await _userService.GetUserByTelegramId(update.Message.Chat.Id);
                    theme = await _botThemeService.GetLocalizedThemeById(user.LastTheme.Id, user.Language);
                }
                await ExecuteExecutor(theme, user, update.Message.Text);
            }
            catch (Exception)
            {
                throw;
            }
            TypedResults.Ok();
        }

        private async Task ExecuteExecutor(BotThemeEntity theme, UserEntity user, string Message)
        {
            Type executorType = Type.GetType("Api.Executors." + theme.ThemeExecutor);
            var args = new object[] { user, Message, _botThemeService, _botClient, _userService, _languageService, _requestService };
            IExecutor Executor = (IExecutor)Activator.CreateInstance(executorType, args: args)!;

            await Executor.Execute();
        }
    }
}
