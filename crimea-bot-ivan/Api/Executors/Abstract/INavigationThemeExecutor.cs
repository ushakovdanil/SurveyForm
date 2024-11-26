using Api.Models.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace Api.Services.Abstract
{
    public interface INavigationThemeExecutor
    {
        Task SendThemeToUser(BotThemeEntity theme);
    }
}
