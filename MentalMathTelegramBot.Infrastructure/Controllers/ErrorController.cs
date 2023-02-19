using MentalMathTelegramBot.Infrastructure.Messages;

namespace MentalMathTelegramBot.Infrastructure.Controllers
{
    public class ErrorController : BaseMessageController
    {
        public override async Task DoAction()
        {
            await SendMessageAsync(new TextMessage("Command not found."));
        }
    }
}
