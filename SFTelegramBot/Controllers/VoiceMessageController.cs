using SFTelegramBot.Configuration;
using SFTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SFTelegramBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler)
        {
            _appSettings = appSettings;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "соси", cancellationToken: ct);
        }
    }
}
