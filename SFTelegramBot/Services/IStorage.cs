using SFTelegramBot.Models;

namespace SFTelegramBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
