using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class TextMessage : QueryMessageKeyboard, IMessage
    {
        public TextMessage(string text)
        {
            Text = text;
        }
    }
}