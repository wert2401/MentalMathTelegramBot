namespace MentalMathTelegramBot.Infrastructure.Messages.Queries
{
    public class QueryKeyboardButton
    {
        public string Text { get; }
        public string Data { get; }

        public QueryKeyboardButton(string text, string data)
        {
            Text = text;
            Data = data;
        }
    }
}
