using Api.Models.Entities;

namespace Api.Services.Abstract
{
    public interface IBotThemeService
    {
        Task<BotThemeEntity> GetLocalizedThemeToNavigate(Guid currentThemeId, string button, Language language);
        Task<BotThemeEntity> GetLocalizedChildThemeByParentThemeId(Guid currentThemeId, Language language);
        Task<BotThemeEntity> GetLocalizedChildThemeByName(Guid currentThemeId, string name, Language language);
        Task<BotThemeEntity> GetLocalizedThemeByName(string name, Language language);
        Task<BotThemeEntity> GetLocalizedThemeById(Guid id, Language language);
        Task<List<string>> GetLocalizedThemeMessages(BotThemeEntity botThemeEntity);

    }
}
