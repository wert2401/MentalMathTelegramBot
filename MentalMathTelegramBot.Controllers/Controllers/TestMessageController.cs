using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/test")]
    public class TestMessageController : BaseMessageController
    {

        public override async Task DoAction()
        {
            Stream stream = File.OpenRead("./test/meme.png");
            Stream stream2 = File.OpenRead("./test/meme2.png");

            var sMsg = new PhotoMessage("test", stream);
            sMsg.AddRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("start", "/start") });

            var msg = await SendMessageAsync(new PhotoMessage("edited message", stream2));

            await Task.Delay(1000);

            await EditMessageAsync(msg, sMsg);
        }
    }
}
