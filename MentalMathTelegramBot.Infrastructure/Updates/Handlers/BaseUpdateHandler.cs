using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates.Handlers
{
    public abstract class BaseUpdateHandler
    {
        protected Bot Bot { get; }
        protected IControllerFactory ControllerFactory { get; }

        protected BaseUpdateHandler(Bot bot, IControllerFactory controllerFactory)
        {
            Bot = bot;
            ControllerFactory = controllerFactory;
        }

        public abstract Task Action();
    }
}
