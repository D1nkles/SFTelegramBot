namespace SFTelegramBot.Services
{
    internal interface IFileHandler
    {
        Task Download(string fileId, CancellationToken ct);
        string Process(string languageCode);
    }
}
