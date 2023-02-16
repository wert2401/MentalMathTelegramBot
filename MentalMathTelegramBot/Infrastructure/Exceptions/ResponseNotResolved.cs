using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    public class ResponseNotResolved : Exception
    {
        public ResponseNotResolved() : base ("Response was not resolved. Response message is not supported.") { }
    }
}
