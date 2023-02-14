using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/hello")]
    public class HelloMessageController : IMessageController
    {
        public IMessage Get()
        {
            return new TextMessage("Hello");
        }
    }
}
