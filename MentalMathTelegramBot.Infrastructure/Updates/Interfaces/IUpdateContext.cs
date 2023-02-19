using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates.Interfaces
{
    public interface IUpdateContext
    {
        public Bot Bot { get; init; }
        /// <summary>
        /// Request message that came from telegram. Edited message becomes <see cref="RequestMessage"/>
        /// </summary>
        public Message RequestMessage { get; set; }
    }
}
