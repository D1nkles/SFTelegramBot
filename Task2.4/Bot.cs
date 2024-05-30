using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using FinalTask.Controllers;
using FinalTask.Services;

namespace FinalTask
{
    class Bot : BackgroundService
    {
        ITelegramBotClient _telegramClient;
        private InlineKeyboardController _keyboardController;
        private TextMessageController _textMessageController;

        public Bot(ITelegramBotClient telegramClient, TextMessageController textMessageController, 
                   InlineKeyboardController inlineKeyboardController)
        {
            _telegramClient = telegramClient;
            _textMessageController = textMessageController;
            _keyboardController = inlineKeyboardController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id,
                            $"Вы отправили сообщение неверного типа!", cancellationToken: cancellationToken);
                        return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery) 
            {
                await _keyboardController.Handle(update.CallbackQuery, cancellationToken);
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением...");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
