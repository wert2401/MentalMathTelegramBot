using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/start")]
    public class StartMessageController : BaseMessageController
    {
        public override async Task DoAction()
        {
            TextMessage messageKeyboard = new TextMessage($"Привет, {Context.RequestMessage.Chat.FirstName}, этот бот поможет тебе в изучении ментальной арифметики");

            messageKeyboard.AddKeyboardRow(new [] { new QueryKeyboardButton("Теория", "/theory") });
            messageKeyboard.AddKeyboardRow(new [] { new QueryKeyboardButton("Тестирование", "/testMenu") });

            await SendMessageAsync(messageKeyboard);
        }
    }
}
