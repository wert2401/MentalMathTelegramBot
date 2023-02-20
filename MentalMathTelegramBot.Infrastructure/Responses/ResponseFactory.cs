using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Exceptions;

namespace MentalMathTelegramBot.Infrastructure.Responses
{
    public class ResponseFactory
    {
        public static BaseResponse CreateResponse(TelegramBotClient botClient, Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken)
        {
            BaseResponse? response = null;

            switch (responseMessage)
            {
                case TextMessage:
                    return new TextMessageResponse(botClient, requestMessage, responseMessage, cancellationToken);
                case PhotoMessage:
                    return new PhotoMessageResponse(botClient, requestMessage, responseMessage, cancellationToken);
            }


            if (response == null)
                throw new ResponseNotResolved();

            return response;
        }
    }
}
