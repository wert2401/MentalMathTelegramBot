﻿using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class QueryMessageKeyboard : IMessage
    {
        public List<IEnumerable<QueryKeyboardButton>> Rows { get; private set; }
        public string Text { get; set; }

        public QueryMessageKeyboard(string text = "")
        {
            Rows = new List<IEnumerable<QueryKeyboardButton>>();
            Text = text;
        }

        public void AddRow(IEnumerable<QueryKeyboardButton> buttons)
        {
            Rows.Add(buttons);
        }

        public InlineKeyboardMarkup ToMarkup()
        {
            return new InlineKeyboardMarkup(Rows.Select(r => r.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Data))));
        }
    }
}