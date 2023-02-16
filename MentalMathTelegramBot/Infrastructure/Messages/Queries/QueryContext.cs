using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Messages.Queries
{
    public class QueryContext : IUpdateContext
    {
        public Bot Bot { get; init; }
        public CallbackQuery Query { get; set; }
        public Message RequestMessage { get; set; }

        public QueryContext(Bot bot, CallbackQuery query, Message requestMessage)
        {
            Bot = bot;
            Query = query;
            RequestMessage = requestMessage;
        }
    }
}
