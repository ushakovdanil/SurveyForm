using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Api.Models.Enums;

namespace Api.Models.Domain
{
    public class BotMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public InlineKeyboardMarkup InlineKeyboardMarkup { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboardMarkup { get; set; }
        public KeyboardType KeyboardType { get; set; }
        public List<InputFile> InputFiles { get; set; }
    }
}