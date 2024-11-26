using Api.Models.Entities.Abstract;

namespace Api.Models.Entities
{
    public class BotThemeEntity : BaseEntity
    {
        public BotThemeEntity? ParentTheme { get; set; }
        public string Name { get; set; }
        public List<BotMessageEntity> BotMessages { get; set; }
        public string ThemeExecutor {  get; set; }
        public string Validator {  get; set; }

        //Required by EF core
        public BotThemeEntity() : base() { }
        public BotThemeEntity(BotThemeEntity parentTheme, string name, List<BotMessageEntity> botMessages, string validator) : base()
        {
            ParentTheme = parentTheme;
            Name = name;
            BotMessages = botMessages;
            Validator = validator;
        }
    }
}
