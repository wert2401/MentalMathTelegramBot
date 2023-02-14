using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/meme")]
    public class TestController : IMessageController
    {
        public IMessage Get()
        {
            Stream stream = File.OpenRead("./test/meme.png");
            return new PhotoMessage("test", stream);
        }
    }
}
