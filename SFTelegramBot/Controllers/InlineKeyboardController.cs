using SFTelegramBot.Configuration;
using SFTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SFTelegramBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                "ru" => "🇷🇺 Русский",
                "en" => "🇬🇧 Английский",
                "ja" => "🇯🇵 Японский",
                _ => string.Empty
            };
            
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Язык аудио - {languageText}.</b>{Environment.NewLine}" +
                $"{Environment.NewLine}Можно изменить в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
