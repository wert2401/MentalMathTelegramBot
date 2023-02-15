using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/meme")]
    public class TestController : BaseMessageController
    {

        public override async Task DoAction()
        {
            Stream stream = File.OpenRead("./test/meme.png");
            Stream stream2 = File.OpenRead("./test/meme2.png");
            var msg = await SendMessageAsync(new PhotoMessage("test", stream));

            await Task.Delay(1000);

            await EditMessageAsync(msg, new PhotoMessage("edited message", stream2));
        }
    }
}
