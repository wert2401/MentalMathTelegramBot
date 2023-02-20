using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using MentalMathTelegramBot.Infrastructure.Updates.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Controllers
{
    public abstract class BaseMessageController : IMessageController
    {
        /// <summary>
        /// Need to be set on resolving controller
        /// </summary>
        public IUpdateContext Context { get; set; }

        /// <summary>
        /// Action of controller for messages without params
        /// </summary>
        /// <returns></returns>
        public virtual Task DoAction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Action of controller for messages with params
        /// </summary>
        /// <returns></returns>
        public virtual Task DoAction(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

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
            Context.RequestMessage = editingMessage;
            return await Context.Bot.EditMessageAsyc(Context, newMessage);
        }
    }
}
