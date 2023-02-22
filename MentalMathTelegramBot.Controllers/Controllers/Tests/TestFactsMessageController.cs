﻿using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Controllers.MessagesData;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers.Controllers.Tests
{
    [Path("/testFacts")]
    public class TestFactsMessageController : BaseMessageController
    {
        private readonly UnitOfWork unitOfWork;
        private readonly MessagesDataHandler dataHandler;

        public TestFactsMessageController(UnitOfWork unitOfWork, MessagesDataHandler dataHandler)
        {
            this.unitOfWork = unitOfWork;
            this.dataHandler = dataHandler;
        }

        public override async Task DoAction()
        {
            var quest = dataHandler.GetRandomFactQuestion();

            unitOfWork.Add(new Data.Models.TestAnswer { UserId = Context.RequestMessage.Chat.Id.ToString(), Answer = quest.Answer, TestType = Test.Facts });

            var msg = new TextMessage(quest.Text);

            if (quest.AnswerVariants != null)
                foreach (string variant in quest.AnswerVariants)
                    msg.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton(variant, variant) });

            await SendMessageAsync(msg);
        }
    }
}
