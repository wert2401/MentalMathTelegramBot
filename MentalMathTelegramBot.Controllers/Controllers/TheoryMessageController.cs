using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers
{
    [Path("/theory")]
    public class TheoryMessageController : BaseMessageController
    {
        private List<PhotoMessage> theoryMessages;

        public TheoryMessageController(MessagesData.MessagesDataHandler dataHandler)
        {
            theoryMessages = new List<PhotoMessage>();

            for (int i = 0; i < dataHandler.TheoryPagesCount; i++)
            {
                var page = dataHandler.GetTheoryPage(i);

                if (page == null)
                    break;

                var curMessage = new PhotoMessage(page.Text, File.OpenRead(page.PhotoFileName));

                if (i == 0)
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Следующая страница", "/theory?page=1") });
                else if (i == dataHandler.TheoryPagesCount - 1)
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Предыдущая страница", $"/theory?page={i - 1}") });
                else
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton>
                {
                    new QueryKeyboardButton("Предыдущая страница", $"/theory?page={i - 1}"),
                    new QueryKeyboardButton("Следующая страница", $"/theory?page={i + 1}")
                });

                curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Вернуться в главное меню", "/start") });

                theoryMessages.Add(curMessage);
            }
        }

        public override async Task DoAction()
        {
            var curMessage = theoryMessages[0];

            await SendMessageAsync(curMessage);
        }

        public override async Task DoAction(Dictionary<string, string> parameters)
        {
            var page = parameters["page"] != null ? int.Parse(parameters["page"]) : 0;

            var curMessage = theoryMessages[page];

            if (Context.RequestMessage.Text != curMessage.Text)
                await EditMessageAsync(Context.RequestMessage, curMessage);
        }
    }
}
