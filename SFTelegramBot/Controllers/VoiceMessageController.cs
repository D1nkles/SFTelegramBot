﻿using SFTelegramBot.Configuration;
using SFTelegramBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SFTelegramBot.Controllers
{
    public class VoiceMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(IStorage memoryStorage, ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler)
        {
            _memoryStorage = memoryStorage;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);

            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);

            string LanguageCode = _memoryStorage.GetSession(message.Chat.Id).LanguageCode;
            _audioFileHandler.Process(LanguageCode);
        }
    }
}
