using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/test")]
    public class TestController : IMessageController
    {
        public IMessage Get()
        {
            return new TextMessage("test");
        }
    }
}
