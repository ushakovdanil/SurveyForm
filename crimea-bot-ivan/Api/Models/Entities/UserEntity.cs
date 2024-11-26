using Api.Models.Entities.Abstract;

namespace Api.Models.Entities
{
    public class UserEntity : BaseEntity
    {
        public long TelegramId { get; set; }
        public string? Username { get; set; }
        public BotThemeEntity LastTheme { get; set; }
        public Language Language { get; set; }

        //Required by EF core
        public UserEntity() { }
        public UserEntity(long telegramId, string username, BotThemeEntity lastTheme, Language language) : base()
        {
            TelegramId = telegramId;
            Username = username;
            LastTheme = lastTheme;
            Language = language;
        }
    }
}