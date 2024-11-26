using Api.Models.Entities.Abstract;

namespace Api.Models.Entities
{
    public class InlineKeyboardButtonEntity : BaseEntity
    {
        public Guid TextId { get; set; }
        public string? Url { get; set; }
        public string? CallbackData { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        //Required by EF core
        public InlineKeyboardButtonEntity()
        {
            
        }
        public InlineKeyboardButtonEntity(Guid textId, string? url, string? callbackData, int row, int column) : base()
        {
            TextId = textId;
            Url = url;
            CallbackData = callbackData;
            Row = row;
            Column = column;
        }
    }
}
