using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Microsoft.Extensions.Logging;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/hello")]
    public class HelloMessageController : IMessageController
    {
        private readonly ILogger<HelloMessageController> logger;

        public HelloMessageController(ILogger<HelloMessageController> logger)
        {
            this.logger = logger;
        }

        public IMessage Get()
        {
            logger.LogInformation("Hello controller");
            return new TextMessage("Hello");
        }
    }
}
