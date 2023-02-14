using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Controllers
{
    public class ErrorController : IMessageController
    {
        IMessage IMessageController.Get()
        {
            return new TextMessage("Command not found.");
        }
    }
}
