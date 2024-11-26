using Api.Models.Entities.Abstract;

namespace Api.Models.Entities
{
    public class ReplyKeyboardButtonEntity : BaseEntity
    {
        public string CallbackValue { get; set; }
        public BotThemeEntity? BotThemeToNavigate { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public List<LocalizebleResource> LocalizedTexts { get; set; }


        //Required by EF core
        public ReplyKeyboardButtonEntity() { }
        public ReplyKeyboardButtonEntity(int column, int row, List<LocalizebleResource> localizedTexts) : base()
        {
            Column = column;
            Row = row;
            LocalizedTexts = localizedTexts;
        }
    }
}