﻿namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IControllerFactory
    {
        public IMessageController ResolveController(string path);
    }
}