using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot.Types.InputFiles;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class PhotoMessage : IMessage
    {
        public string Text { get; set; }
        public InputOnlineFile Photo { get; private set; }

        public PhotoMessage(string text, Stream photoStream)
        {
            Text = text;
            Photo = new InputOnlineFile(photoStream);
        }
    }
}