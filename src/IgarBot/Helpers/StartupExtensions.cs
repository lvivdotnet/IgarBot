using IgarBot.Configurations;
using Microsoft.Extensions.DependencyInjection;
using MihaZupan;
using Telegram.Bot;

namespace IgarBot.Helpers
{
    public static class StartupExtensions
    {
        public static void AddTelegram(this IServiceCollection collection, BotConfiguration config)
        {
            var client = string.IsNullOrEmpty(config.Socks5Host)
                ? new TelegramBotClient(config.BotToken)
                : new TelegramBotClient(config.BotToken, new HttpToSocks5Proxy(config.Socks5Host, config.Socks5Port));

            collection.AddSingleton(client);
        }
    }
}