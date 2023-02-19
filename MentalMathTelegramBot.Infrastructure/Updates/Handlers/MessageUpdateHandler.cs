using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates.Handlers
{
    public class MessageUpdateHandler : BaseUpdateHandler
    {
        private readonly Message message;

        public MessageUpdateHandler(Bot bot, Message message, IControllerFactory controllerFactory) : base(bot, controllerFactory)
        {
            this.message = message;
        }

        public override async Task Action()
        {
            IMessageController messageController = ControllerFactory.ResolveController(message.Text!);

            messageController.Context = new MessageContext(Bot, message);

            await messageController.DoAction();
        }
    }
}
