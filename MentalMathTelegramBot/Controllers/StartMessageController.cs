using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/start")]
    public class StartMessageController : BaseMessageController
    {
        public override async Task DoAction()
        {
            QueryMessageKeyboard messageKeyboard = new QueryMessageKeyboard($"Hello, {Context.RequestMessage.From?.FirstName} this bot will help you to learn mental math");

            messageKeyboard.AddRow(new [] { new QueryKeyboardButton("Learn", "/learn"), new QueryKeyboardButton("Test", "/test") });

            await SendMessageAsync(messageKeyboard);
        }
    }
}
