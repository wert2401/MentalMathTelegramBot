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
            (var path, var parameters) = GetPathAndParameters(query.Data!);

            IMessageController messageController = ControllerFactory.ResolveController(path);

            messageController.Context = new QueryContext(Bot, query, query.Message!);

            if (parameters != null)
                await messageController.DoAction(parameters);
            else
                await messageController.DoAction();
        }
    }
}
