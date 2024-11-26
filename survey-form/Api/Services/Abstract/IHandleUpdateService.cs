using Telegram.Bot.Types;

namespace Api.Services.Abstract
{
    public interface IHandleUpdateService
    {
        Task HandleUpdateAsync(Update update);
    }
}
