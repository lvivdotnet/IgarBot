using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IgarBot.TelegramServices
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
                var text = update.Message.Type switch
                {
                    MessageType.Text => Guid.NewGuid().ToString(),
                    MessageType.ChatMembersAdded => ($"Hello @{update.Message.From.Username}. {Environment.NewLine}" +
                                                     $"Guid for you: {Guid.NewGuid()}"),
                    _ => null
                };

                if (text != null)
                {
                    await _telegramService.SendTextMessageAsync(update.Message.Chat.Id, text + Environment.NewLine +
                                                                                        HeartEmoji);
                }
            }
        }
    }
}