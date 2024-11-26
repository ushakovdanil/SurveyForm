using Api.Models.Entities.Abstract;
using Api.Models.Enums;
using Telegram.Bot.Types.Enums;

namespace Api.Models.Entities
{
    public class BotMessageEntity : BaseEntity
    {
        public InlineKeyboardButtonEntity[]? InlineKeyboardMarkup { get; set; }
        public List<ReplyKeyboardButtonEntity> ReplyKeyboardMarkup { get; set; }
        public string? KeyboardType { get; set; }
        public List<string>? FilesUrls { get; set; }
        public List<LocalizebleResource> LocalizedTexts { get; set; }
        public BotMessageType BotMessageType { get; set; }

        //Required by EF core
        public BotMessageEntity() { }
        public BotMessageEntity(List<LocalizebleResource> localizedTexts, 
            InlineKeyboardButtonEntity[] inlineKeyboardMarkup, 
            List<ReplyKeyboardButtonEntity> replyKeyboardMarkup, 
            string keyboardType, 
            List<string> filesUrls) : base()
        {
            LocalizedTexts = localizedTexts;
            InlineKeyboardMarkup = inlineKeyboardMarkup;
            ReplyKeyboardMarkup = replyKeyboardMarkup;
            KeyboardType = keyboardType;
            FilesUrls = filesUrls;
        }
    }
}