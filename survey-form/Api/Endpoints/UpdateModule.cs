//using Api.Models.Entities;
//using Api.Services;
//using Api.Services.Abstract;
//using Carter;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace Api.Endpoints
//{
//    public class UpdateModule : ICarterModule
//    {
//        public void AddRoutes(IEndpointRouteBuilder app)
//        {
//            app.MapPost("handle_update", HandleUpdate);
//        }
 
//        public static async Task<IResult> HandleUpdate(
//            Update update,
//            ILogger<UpdateModule> logger,
//            IUserService userService,
//            IBotThemeService botThemeService,
//            ILanguageService languageService,
//            ITelegramBotClient bot)
//        {
//            try
//            {
//                BotThemeEntity theme;
//                UserEntity user;
//                var defaultLanguage = await languageService.GetLanguageByName("Ukrainian");
//                var defaultTheme = await botThemeService.GetLocalizedThemeById(Guid.Parse("0abe94e1-6953-49a7-bfb6-1ae5cc3a2aec"), defaultLanguage);
//                if (await userService.IsUserByTelegramIdExist(update.Message.Chat.Id) == false)
//                {
//                    theme = defaultTheme;
//                    user = new UserEntity(
//                        update.Message.Chat.Id,
//                        update.Message.Chat.Username,
//                        theme,
//                        defaultLanguage
//                        );
//                    userService.AddUser(user);
//                }
//                else
//                {
//                    user = await userService.GetUserByTelegramId(update.Message.Chat.Id);
//                    theme = await botThemeService.GetLocalizedThemeById(user.LastTheme.Id, user.Language);
//                }
//                if (update.Message.Text == "/start")
//                {
//                    user = await userService.GetUserByTelegramId(update.Message.Chat.Id);
//                    theme = await botThemeService.GetLocalizedThemeById(Guid.Parse("0abe94e1-6953-49a7-bfb6-1ae5cc3a2aec"), defaultLanguage);
//                }
//                NavigationThemeExecutor navigationThemeExecutor = new NavigationThemeExecutor(user, update.Message.Text, botThemeService, bot, userService);
//                await navigationThemeExecutor.Execute();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//            return TypedResults.Ok();
//        }
//    }
//}
