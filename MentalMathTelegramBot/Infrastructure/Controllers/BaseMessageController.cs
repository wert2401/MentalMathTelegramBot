﻿using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Controllers
{
    public abstract class BaseMessageController : IMessageController
    {
        /// <summary>
        /// Need to be set on resolving controller
        /// </summary>
        public MessageContext Context { get; set; }

        public abstract Task DoAction();

        protected async Task<Message> SendMessageAsync(IMessage message)
        {
            return await Context.Bot.SendMessageAsync(Context, message);
        }

        /// <summary>
        /// Edit sended message
        /// </summary>
        /// <param name="editingMessage">Message that was sent and wanted to be edited</param>
        /// <param name="newMessage">New message</param>
        /// <returns></returns>
        protected async Task<Message> EditMessageAsync(Message editingMessage, IMessage newMessage)
        {
            return await Context.Bot.EditMessageAsyc(editingMessage, newMessage);
        }
    }
}