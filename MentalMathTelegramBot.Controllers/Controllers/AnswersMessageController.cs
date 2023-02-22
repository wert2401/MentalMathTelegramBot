using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Controllers.Data.Models;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;
using MentalMathTelegramBot.Infrastructure.Updates;

namespace MentalMathTelegramBot.Controllers.Controllers
{
    [Path("*")]
    public class AnswersMessageController : BaseMessageController
    {
        private readonly UnitOfWork unitOfWork;

        public AnswersMessageController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public override async Task DoAction()
        {
            var rightAnswer = unitOfWork.GetAll<TestAnswer>(a => a.UserId == Context.RequestMessage.Chat.Id.ToString()).OrderByDescending(a => a.Id).FirstOrDefault();

            if (rightAnswer == null)
            {
                await SendMessageAsync(new TextMessage("Неизвестная команда"));
                return;
            }

            TextMessage msg;

            string userAnswer = Context.RequestMessage.Text ?? "";

            if (Context is QueryContext queryContext)
                userAnswer = queryContext.Query.Data ?? "";

            if (userAnswer == rightAnswer.Answer)
            {
                msg = new TextMessage("Правильно!");
            }
            else
            {
                msg = new TextMessage($"Не правильно! \n Правильный ответ: {rightAnswer.Answer}");
            }

            switch (rightAnswer.TestType)
            {
                case Tests.Test.SimpleRule:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testRuleSimple") });
                    break;
                case Tests.Test.AbacusNumber:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testAbacusNumbers") });
                    break;
                case Tests.Test.Theory:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testTheory") });
                    break;
                case Tests.Test.Facts:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testFacts") });
                    break;
            }

            msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Окончить тестирование", "/start") });

            await SendMessageAsync(msg);
        }
    }
}
