using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    public class ControllerNotFoundException : Exception
    {
        public ControllerNotFoundException() : base("Controller not found") { }
    }
}
