using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Updates.Handlers;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Updates
{
    public class UpdateHandlersFactory
    {
        public static BaseUpdateHandler CreateUpdateHandler(Bot bot, Update update, IControllerFactory controllerFactory)
        {
            BaseUpdateHandler? updateHandler = null;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message is { } message)
                if (message.Date <= bot.StartedTime || message.Text is { } messageText)
                    return new MessageUpdateHandler(bot, message, controllerFactory);

            if (update.CallbackQuery is { } query)
                if (query.Message?.Date <= bot.StartedTime || query.Data is { } messageText || query.Message != null)
                    return new QueryUpdateHandler(bot, query, controllerFactory);


            if (updateHandler == null)
                throw new UpdateNotHandledException(update.Type);

            return updateHandler;
        }
    }
}
