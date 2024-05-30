using FinalTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinalTask.Controllers
{
    internal class InlineKeyboardController
    {
        ITelegramBotClient _telegramClient;
        IStorage _memoryStorage;
        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage storage) 
        {
            _telegramClient = telegramClient;
            _memoryStorage = storage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken) 
        {
            if (callbackQuery?.Data == null) 
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).operationType = callbackQuery.Data;

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, 
                "Теперь введите в сообщении данные, удовлетворяющие выполнению выбранной операции.", 
                cancellationToken: cancellationToken);
        }
    }
}

