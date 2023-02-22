using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Controllers.MessagesData;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;

namespace MentalMathTelegramBot.Controllers.Controllers.Tests
{
    [Path("/testAbacusNumbers")]
    public class TestAbacusNumbersMessageController : BaseMessageController
    {
        private readonly UnitOfWork unitOfWork;
        private readonly MessagesDataHandler dataHandler;

        public TestAbacusNumbersMessageController(UnitOfWork unitOfWork, MessagesDataHandler dataHandler)
        {
            this.unitOfWork = unitOfWork;
            this.dataHandler = dataHandler;
        }

        public override async Task DoAction()
        {
            var quest = dataHandler.GetRandomAbacusNumberQuestion();

            unitOfWork.Add(new Data.Models.TestAnswer { UserId = Context.RequestMessage.Chat.Id.ToString(), Answer = quest.Answer, TestType = Test.AbacusNumber });

            await SendMessageAsync(new PhotoMessage("Какое число представлено на абакусе?", File.OpenRead(quest.PhotoFileName)));
        }
    }
}
