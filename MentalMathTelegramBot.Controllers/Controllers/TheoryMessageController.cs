﻿using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;

namespace MentalMathTelegramBot.Controllers
{
    public static class TheoryStrings
    {
        public static List<string> Strings = new List<string>
        {
            "<b>Ментальная арифметика</b> – метод обучения устному счету с использование счетов абакус. \r\nОсновной инструмент для быстрого счета в ментальной арифметике - это абакус (соробан) или по простому счеты (подробнее об этом далее).\r\nОчень важно научить ребенка представлять (визуализировать) операции с абакусом. Чтобы все вычисления производились в уме и в виде образов.\r\nПочему это важно?\r\n1) Абакус далеко не всегда может быть под рукой.\r\n2) Это развивает визуальное мышление.\r\n3) Это делает скорость счета фантастически быстрой.\r\nСмысл счета в том, чтобы считать не единицами и по пальцам, а научиться разбивать числа на простые группы (составляющие), превратить их в мысленный образ и произвести ряд простых вычислений вместо сложных.\r\nАлгоритм получения навыка состоит из двух основных этапов.\r\n",
            "<b>1 этап. Понимаем принцип и считаем на реальном абакусе.</b> \r\nОчень важно сначала отработать механику счета на абакусе. Понять смысл, как это делается и довести навык счета до автоматизма.\r\nЖелательно дублировать все примеры, которые будут здесь на реальном абакусе. Вот ссылка на сайт алиэкспресс, где можно купить дешевый абакус.\r\nСчет на реальном абакусе - это отличная гимнастика для пальцев.\r\nЕсли же возможности купить реальный абакус у вас нет, можете открыть в соседней вкладке виртуальный абакус и пользоваться им.",
            "<b>2 этап. Учимся представлять и производить операции в уме.</b> \r\nПосле того, как навык счета на реальном абакусе закреплен, желательно переходить к работе с образами и убрать из зоны доступности этот инструмент.\r\nОсновная задача этого этапа начинать считать в уме представляя себе операции на абакусе.\r\nИменно после этого этапа, овладев навыком счета в уме, мы и получаем многократное ускорение скорости счета.\r\nНу, и напоследок, желательно также зарегистрироваться на этом сайте, чтобы вы могли видеть ваши успехи по прохождению заданий.\r\nУспехов в освоении нового и очень нужного навыка быстрого счета\r\n",
            "<b>Из чего состоит Абакус – Соробан?</b> \r\n1)Точка\r\n2)Верхние косточки\r\n3)Рамка\r\n4)Перекладина\r\n5)Спица\r\n6)Нижние косточки\r\n",
        };
    }

    [Path("/theory")]
    public class TheoryMessageController : BaseMessageController
    {
        private List<TextMessage> theoryMessages = new List<TextMessage> {
            new TextMessage(TheoryStrings.Strings[0]),
            new TextMessage(TheoryStrings.Strings[1]),
            new TextMessage(TheoryStrings.Strings[2]),
            new TextMessage(TheoryStrings.Strings[3]),
        };

        public TheoryMessageController()
        {
            for (int i = 0; i < theoryMessages.Count; i++)
            {
                var curMessage = theoryMessages[i];

                if (i == 0)
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Следующая страница", "/theory?page=1") });
                else if (i == theoryMessages.Count - 1)
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Предыдущая страница", $"/theory?page={i - 1}") });
                else
                    curMessage.AddKeyboardRow(new List<QueryKeyboardButton>
                {
                    new QueryKeyboardButton("Предыдущая страница", $"/theory?page={i - 1}"),
                    new QueryKeyboardButton("Следующая страница", $"/theory?page={i + 1}")
                });

                curMessage.AddKeyboardRow(new List<QueryKeyboardButton> { new QueryKeyboardButton("Вернуться в главное меню", "/start") });
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

            await EditMessageAsync(Context.RequestMessage, curMessage);
        }
    }
}