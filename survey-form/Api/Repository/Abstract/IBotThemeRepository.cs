using Api.Models.Entities;

namespace Api.Repository.Abstract
{
    public interface IBotThemeRepository
    {
        Task<BotThemeEntity> GetBotThemeById(Guid id);
        /*
        Task<BotThemeEntity> AddBotTheme(BotThemeEntity botThemeEntity);
        Task UpdateBotTheme(BotThemeEntity botThemeEntity);
        Task DeleteBotTheme(Guid id);
        */
    }
}
