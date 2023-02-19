using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates.Handlers
{
    public class QueryUpdateHandler : BaseUpdateHandler
    {
        private readonly CallbackQuery query;

        public QueryUpdateHandler(Bot bot, CallbackQuery query, IControllerFactory controllerFactory) : base(bot, controllerFactory)
        {
            this.query = query;
        }

        public override async Task Action()
        {
            IMessageController messageController = ControllerFactory.ResolveController(query.Data!);

            messageController.Context = new QueryContext(Bot, query, query.Message!);

            await messageController.DoAction();
        }
    }
}
