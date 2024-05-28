using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SFTelegramBot
{
    class Bot
    {
        ITelegramBotClient _telegramClient;

        public Bot(ITelegramBotClient telegramClient) 
        {
            _telegramClient = telegramClient;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) 
        {
            if (update.Type == UpdateType.CallbackQuery) 
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку", 
                    cancellationToken: cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message) 
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы отправили сообщение", 
                    cancellationToken: cancellationToken);
                return;
            }
        }
    }
}
