using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace IgarBot.TelegramServices
{
    public sealed class TelegramMessageHandler
    {
        private readonly TelegramBotClient _telegramService;
        private readonly IRepository<Igar> _repository;
        private const string HeartEmoji = "\U0001F496";

        public TelegramMessageHandler(
            TelegramBotClient telegramService, IRepository<Igar> repository)
        {
            _telegramService = telegramService;
            _repository = repository;
        }

        public async Task Handle(Update update)
        {
            if (update.Type is UpdateType.Message)
            {
                var igarGuid = await _repository.GetIgar().GenerateGuidCachedSafeAsyncOrThrowOutOfWindow();
                var text = update.Message.Type switch
                {
                    MessageType.Text => igarGuid.ToString(),
                    MessageType.ChatMembersAdded => $"Hello @{update.Message.From.Username}. {Environment.NewLine}" +
                                                    $"Guid for you: {igarGuid}",
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
