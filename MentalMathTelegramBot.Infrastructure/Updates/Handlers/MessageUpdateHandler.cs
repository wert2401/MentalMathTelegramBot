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
            (var path, var parameters) = GetPathAndParameters(message.Text!);

            IMessageController messageController = ControllerFactory.ResolveController(path);

            messageController.Context = new MessageContext(Bot, message);

            if (parameters != null)
                await messageController.DoAction(parameters);
            else
                await messageController.DoAction();
        }
    }
}
