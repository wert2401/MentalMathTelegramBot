using MentalMathTelegramBot.Infrastructure.Updates.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates
{
    public class MessageContext : IUpdateContext
    {
        public Bot Bot { get; init; }
        public Message RequestMessage { get; set; }

        public MessageContext(Bot bot, Message requestMessage)
        {
            Bot = bot;
            RequestMessage = requestMessage;
        }
    }
}
