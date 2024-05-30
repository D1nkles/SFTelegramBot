using FinalTask.Models;

namespace FinalTask.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
