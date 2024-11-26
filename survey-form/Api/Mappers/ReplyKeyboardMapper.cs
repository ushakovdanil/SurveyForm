using Api.Models.Domain;
using Api.Models.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace Api.Mappers
{
    public static class ReplyKeyboardMapper
    {
        public static ReplyKeyboardMarkup ToDomain(this List<ReplyKeyboardButtonEntity> buttons)
        {
            int rowCount = buttons.Max(r => r.Row);
            var keyboard = new KeyboardButton[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                var row = buttons.Where(b => b.Row == i+1).OrderBy(x => x.Column);
                var keyboardbuttons = new KeyboardButton[row.Count()];
                for (int j = 0; j < row.Count(); j++)
                {
                    var button = new KeyboardButton(row.Where(x => x.Column == j + 1).FirstOrDefault().LocalizedTexts.FirstOrDefault().Value) ;
                    keyboardbuttons[j] = button;
                }
                keyboard[i] = keyboardbuttons;
            }
            return new ReplyKeyboardMarkup(keyboard)
            {
                ResizeKeyboard = true
            };
        }
    }
}