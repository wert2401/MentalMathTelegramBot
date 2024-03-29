﻿using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers.Controllers.Tests
{
    public enum Test
    {
        SimpleRule,
        AbacusNumber,
        Theory,
        Facts
    }

    [Path("/testMenu")]
    public class TestMenuMessageController : BaseMessageController
    {
        public override async Task DoAction()
        {
            var msg = new TextMessage("Выберите тест, который хотите пройти");
            msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Тест по теории", "/testTheory") });
            msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Тест \"правда или ложь\"", "/testFacts") });
            msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Числа на абакусе", "/testAbacusNumbers") });
            msg.AddKeyboardRow(new List<QueryKeyboardButton>() { new QueryKeyboardButton("Правило просто", "/testRuleSimple") });

            await SendMessageAsync(msg);
        }
    }
}
