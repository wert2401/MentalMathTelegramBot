using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot.Types.InputFiles;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class PhotoMessage : QueryMessageKeyboard, IMediaMessage
    {
        public InputOnlineFile Photo { get; private set; }
        /// <summary>
        /// Represents data stream of media message
        /// </summary>
        public Stream Stream { get; set; }

        public PhotoMessage(string text, Stream photoStream)
        {
            Text = text;
            Stream = photoStream;
            Photo = new InputOnlineFile(photoStream);
        }

        public bool HasMarkup { get => Rows.Count > 0; }
    }
}