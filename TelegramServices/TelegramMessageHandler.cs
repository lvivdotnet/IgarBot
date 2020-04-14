using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GuidBot.TelegramServices
{
    public class TelegramMessageHandler
    {
        private readonly TelegramBotClient _telegramService;
        private const string HeartEmoji = "\U0001F496";

        public TelegramMessageHandler(
            TelegramBotClient telegramService)
        {
            _telegramService = telegramService;
        }

        public async Task Handle(Update update)
        {
            if (update.Type == UpdateType.Message)
            {
                var chatId = update.Message.Chat.Id;

                await _telegramService.SendTextMessageAsync(chatId, Guid.NewGuid() + Environment.NewLine + HeartEmoji);
            }
        }
    }
}