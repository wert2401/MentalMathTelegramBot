using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IUpdateContext
    {
        public Bot Bot { get; init; }
        public Message RequestMessage { get; init; }
    }
}
