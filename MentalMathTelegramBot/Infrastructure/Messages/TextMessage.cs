using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class TextMessage : IMessage
    {
        public string Text { get; set; }

        public TextMessage(string text)
        {
            Text = text;
        }
    }
}