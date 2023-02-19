using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;

namespace MentalMathTelegramBot.Controllers.Controllers
{
    [Path("*")]
    public class AnswersMessageController : BaseMessageController
    {
        public override async Task DoAction()
        {
            if (Context.RequestMessage.Text == "15")
                await SendMessageAsync(new TextMessage("Правильно!"));
            else
                await SendMessageAsync(new TextMessage("Не правильно!"));
        }
    }
}
