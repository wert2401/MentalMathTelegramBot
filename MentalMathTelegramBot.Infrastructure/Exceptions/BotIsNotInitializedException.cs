using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    public class BotIsNotInitializedException : Exception
    {
        public BotIsNotInitializedException() : base("Bot is not initialized. First call Init().") { }
    }
}
