using System.Threading.Tasks;
using IgarBot.TelegramServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace IgarBot.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : Controller
    {
        private readonly TelegramMessageHandler _telegramMessageHandler;
        private readonly ILogger<TelegramController> _logger;

        public TelegramController(
            TelegramMessageHandler telegramMessageHandler,
            ILogger<TelegramController> logger)
        {
            _telegramMessageHandler = telegramMessageHandler;
            _logger = logger;
        }

        [HttpPost("update/{token}")]
        public async Task Update([FromRoute] string token, [FromBody] Update update)
        {
            _logger.LogInformation("We received new {@message}", update);
            await _telegramMessageHandler.Handle(update);
        }
    }
}