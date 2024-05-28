using SFTelegramBot.Models;

namespace SFTelegramBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
