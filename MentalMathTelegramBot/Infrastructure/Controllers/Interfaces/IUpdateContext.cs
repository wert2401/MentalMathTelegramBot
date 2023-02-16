using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IUpdateContext
    {
        public Bot Bot { get; init; }
        /// <summary>
        /// Updated after editing message. Edited message becomes <see cref="RequestMessage"/>
        /// </summary>
        public Message RequestMessage { get; set; }
    }
}
