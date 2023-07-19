using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp7
{
    class Keyboard
    {
        public static InlineKeyboardMarkup GetKeyboard()
        {
            var kbrd = new
            InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("Данные о студенте", "infoStudent") },
                    new[]
                    {
                         InlineKeyboardButton.WithCallbackData("Данные об успеваемости", "infoMark")
                    }
            });
            return kbrd;
        }
    }
}
