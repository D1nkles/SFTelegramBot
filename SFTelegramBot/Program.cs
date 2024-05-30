using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFTelegramBot.Configuration;
using SFTelegramBot.Controllers;
using SFTelegramBot.Services;
using System.Text;
using Telegram.Bot;

namespace SFTelegramBot
{
    class Program
    {
        static void ConfigureServices(IServiceCollection services) 
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            services.AddSingleton<IFileHandler, AudioFileHandler>();

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings() 
        {
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\Dinkles\\Downloads",
                BotToken = "7455469646:AAGgh25oMYcY7U4lZ5AhY9M2ocP5Q2v_9ZA",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                InputAudioBitRate = 48000,
                OutputAudioFormat = "wav"
            };
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

    }
}