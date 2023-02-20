using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public abstract class QueryMessageKeyboard : IMessage
    {
        public List<IEnumerable<QueryKeyboardButton>> Rows { get; private set; } = new List<IEnumerable<QueryKeyboardButton>>();
        public string Text { get; set; }

        public QueryMessageKeyboard(string text = "")
        {
            Text = text;
        }

        public void AddRow(IEnumerable<QueryKeyboardButton> buttons)
        {
            Rows.Add(buttons);
        }

        public InlineKeyboardMarkup GetMarkup()
        {
            return new InlineKeyboardMarkup(Rows.Select(r => r.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Data))));
        }

        public bool HasMarkup { get => Rows.Count > 0; }
    }
}
