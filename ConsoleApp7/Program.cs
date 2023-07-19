using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot.Types.ReplyMarkups;

using ConsoleApp7;
using System.Diagnostics.Eventing.Reader;
using System.Data;
using System.Linq.Expressions;
using Telegram.Bots.Http;
using Telegram.Bots.Types;

namespace TelegramBotExperiments
{
    
    class Program
    {
        ConnectDB db = new ConnectDB();
        static Dictionary<string, object> param = new Dictionary<string, object>();
        static SQLPost post = new SQLPost();
        static string lastCommand = "";
        static ITelegramBotClient bot = new TelegramBotClient("6340653203:AAHM_gdjtBf8NWZLHZSzYiZ4BJYNrDCy1tg");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

              
                {
                    if (message.Text.ToLower() == "/start") { botClient.SendTextMessageAsync(message.Chat, "Приветствую!👋 Данный бот предназначен для поиска информации об одногруппниках.🔍👀", replyMarkup: Keyboard.GetKeyboard()); return; }


                    else if (message.Text.ToLower() == "/menu") 
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Меню", replyMarkup: Keyboard.GetKeyboard());
                        return;
                    }
                    else if (lastCommand == "infoStudent")
                    {
                        param.Clear();
                        param.Add("@fio", message.Text);
                        var table = post.commandSelect($"SELECT * from Информация_о_студенте where Студент_ФИО = @fio", param);
                        if (table.Rows.Count == 1)
                        {
                            string answer = "";
                            answer +=  "ФИО: " + table.Rows[0][0] + $"\r\n" 
                                + "Паспорт:" + Convert.ToString(table.Rows[0][1])+$"\r\n" 
                                + "Номер зачётной книжки:" + Convert.ToString(table.Rows[0][2])+$"\r\n" 
                                + "Курс:" + Convert.ToString(table.Rows[0][3]) + $"\r\n"
                                + "Специальность:" + Convert.ToString(table.Rows[0][4]) + $"\r\n" 
                                + "Номер группы:" + Convert.ToString(table.Rows[0][5]) + $"\r\n"
                                + "Номер телефона:" + Convert.ToString(table.Rows[0][6]) + $"\r\n"
                                + "Почта:" + Convert.ToString(table.Rows[0][7]) + $"\r\n" 
                                + "Статус обучения:" + Convert.ToString(table.Rows[0][8]) + $"\r\n"
                                + "Семейное положение:" + Convert.ToString(table.Rows[0][9]) + $"\r\n" 
                                + "Военный билет:" + Convert.ToString(table.Rows[0][10]) + $"\r\n"
                                + "ИНН:" + Convert.ToString(table.Rows[0][11]) + $"\r\n";

                            await botClient.SendTextMessageAsync(message.Chat, answer);
                            await botClient.SendTextMessageAsync(message.Chat, "Меню", replyMarkup: Keyboard.GetKeyboard());
                        }
                        lastCommand = "";
                    }
                    else if (lastCommand == "infoMark")
                    {
                        param.Clear();
                        param.Add("@numb", message.Text);
                        var table = post.commandSelect($"SELECT * from Информация_об_успеваемости where Номер_зачётной_книжки = @numb", param);
                        if (table.Rows.Count == 1)
                        {
                            string answer = "";
                            answer += "Дисциплина " + table.Rows[0][0] + $"\r\n"
                                + "Номер_зачётной книжки:" + Convert.ToString(table.Rows[0][1]) + $"\r\n"
                                + "Баллы:" + Convert.ToString(table.Rows[0][2]) + $"\r\n"
                                + "Оценка:" + Convert.ToString(table.Rows[0][3]) + $"\r\n"
                                + "Форма_аттестации:" + Convert.ToString(table.Rows[0][4]) + $"\r\n"
                                + "Преподаватель:" + Convert.ToString(table.Rows[0][5]) + $"\r\n";

                            await botClient.SendTextMessageAsync(message.Chat, answer);
                            await botClient.SendTextMessageAsync(message.Chat, "Меню", replyMarkup: Keyboard.GetKeyboard());
                        }
                        lastCommand = "";
                    }

                    else
            {
                        await botClient.SendTextMessageAsync(message.Chat, "❌Некорректный ввод❌"); 
                         await botClient.SendTextMessageAsync(message.Chat, "Доступные команды📖", replyMarkup: Keyboard.GetKeyboard()); 
                return;
            }
        }
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
            var message = update.CallbackQuery.Data;
            switch (message)
            {
                case "infoStudent":
                {
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, "Введите ФИО студента👨‍🎓");
                    lastCommand = message;
                    return;
                }
                case "infoMark":
                {
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, "Введите номер зачётной книжки 🪪");
                    lastCommand = message;
                    return;
                }
            }
        }
    }
public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
   
    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
}


static void Main(string[] args)
{
    Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

    var cts = new CancellationTokenSource();
    var cancellationToken = cts.Token;
    var receiverOptions = new ReceiverOptions
    {
        AllowedUpdates = { }, 
    };
    bot.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken
    );
    Console.ReadLine();
}
}
}