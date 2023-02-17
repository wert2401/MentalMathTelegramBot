using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    public class TokenIsNullException : Exception
    {
        public TokenIsNullException() : base("Bot API token not found") { }
    }
}
