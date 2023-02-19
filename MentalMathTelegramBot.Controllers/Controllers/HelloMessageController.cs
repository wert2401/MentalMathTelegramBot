using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using Microsoft.Extensions.Logging;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/learn")]
    public class HelloMessageController : BaseMessageController
    {
        private readonly ILogger<HelloMessageController> logger;

        public HelloMessageController(ILogger<HelloMessageController> logger)
        {
            this.logger = logger;
        }

        public override async Task DoAction()
        {
            logger.LogInformation("Hello controller");

            var msg = await SendMessageAsync(new TextMessage("Hello"));

            await Task.Delay(500);

            await EditMessageAsync(msg, new TextMessage("edited message"));
        }

        public override async Task DoAction(Dictionary<string, string>? parameters = null)
        {
            logger.LogInformation("Hello controller");

            var msg = await SendMessageAsync(new TextMessage("Hello"));

            await Task.Delay(500);

            await EditMessageAsync(msg, new TextMessage(parameters != null ? parameters["test"] : "no"));
        }
    }
}
