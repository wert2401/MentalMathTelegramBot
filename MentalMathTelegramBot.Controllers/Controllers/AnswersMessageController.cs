using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers.Controllers
{
    [Path("*")]
    public class AnswersMessageController : BaseMessageController
    {
        private readonly BotDbContext dbContext;

        public AnswersMessageController(BotDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task DoAction()
        {
            var rightAnswer = dbContext.Answers.Where(a => a.UserId == Context.RequestMessage.Chat.Id.ToString()).OrderByDescending(a => a.Id).FirstOrDefault();

            if (rightAnswer == null)
            {
                await SendMessageAsync(new TextMessage("Неизвестная команда"));
                return;
            }

            if (Context.RequestMessage.Text == rightAnswer.Answer)
            {
                var msg = new TextMessage("Правильно!");
                switch (rightAnswer.TestType)
                {
                    case Tests.Test.SimpleRule:
                        msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testRuleSimple") });
                        break;
                }
                await SendMessageAsync(msg);
            }
            else
            {
                var msg = new TextMessage("Не правильно!");

                switch (rightAnswer.TestType)
                {
                    case Tests.Test.SimpleRule:
                        msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testRuleSimple") });
                        break;
                }

                await SendMessageAsync(msg);
            }
        }
    }
}
