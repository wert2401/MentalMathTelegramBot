using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Messages
{
    public class MessageContext
    {
        public Bot Bot { get; init; }
        public Message RequestMessage { get; init; }

        public MessageContext(Bot bot, Message requestMessage)
        {
            Bot = bot;
            RequestMessage = requestMessage;
        }
    }
}
