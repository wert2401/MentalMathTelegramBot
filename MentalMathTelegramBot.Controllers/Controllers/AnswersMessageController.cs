using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Controllers.Data.Models;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

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

            if (Context.RequestMessage.Text == rightAnswer.Answer)
            {
                msg = new TextMessage("Правильно!");
            }
            else
            {
                msg = new TextMessage("Не правильно!");
            }

            switch (rightAnswer.TestType)
            {
                case Tests.Test.SimpleRule:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testRuleSimple") });
                    break;
                case Tests.Test.AbacusNumber:
                    msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Новый вопрос", "/testAbacusNumbers") });
                    break;
            }

            await SendMessageAsync(msg);
        }
    }
}
