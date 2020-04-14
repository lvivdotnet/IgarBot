using System.Threading;
using System.Threading.Tasks;
using IgarBot.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace IgarBot.TelegramServices
{
    public sealed class TelegramHostedService : IHostedService
    {
        private readonly TelegramBotClient _client;
        private readonly BotConfiguration _configuration;
        private readonly ILogger<TelegramHostedService> _logger;

        public TelegramHostedService(
            TelegramBotClient client,
            BotConfiguration configuration,
            ILogger<TelegramHostedService> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.DeleteWebhookAsync(cancellationToken);
            _logger.LogInformation("Setting webhook with {url} {token}", _configuration.WebhookUrl,
                _configuration.BotToken);
            await _client.SetWebhookAsync($"{_configuration.WebhookUrl}/{_configuration.BotToken}",
                cancellationToken: cancellationToken);
            _logger.LogInformation("End setting webhook");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _client.DeleteWebhookAsync(cancellationToken);
        }
    }
}