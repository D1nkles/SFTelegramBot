using FinalTask.Services;
using FinalTask.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinalTask.Controllers
{
    internal class TextMessageController
    {
        ITelegramBotClient _telegramClient;
        IStorage _memoryStorage;
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage storage) 
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = storage;
        }

        public async Task Handle(Message message, CancellationToken cancellationToken) 
        {
            switch (message.Text) 
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[] 
                    {
                        InlineKeyboardButton.WithCallbackData("Подсчитать количество символов в тексте", "charCount"),
                        InlineKeyboardButton.WithCallbackData("Вычислить сумму целых чисел, введенных через пробел", "sumCount")
                    } );

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, 
                        "Выберите какое действие хотите выполнить:", replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    switch (_memoryStorage.GetSession(message.Chat.Id).operationType) 
                    {
                        case "charCount":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина отправленного вами сообщения - {CharCounter.CountChars(message)}");
                            break;
                        case "sumCount":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма введенных вами чисел - {SumCounter.CountSum(message)}");
                            break;
                        case "None":
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Введите команду /start, чтобы начать работу с ботом",
                        cancellationToken: cancellationToken);
                            break;
                    }
                    break;
            }
        }
    }
}
