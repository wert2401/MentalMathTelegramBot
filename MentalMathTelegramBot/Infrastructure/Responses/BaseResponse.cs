using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Responses
{
    public abstract class BaseResponse
    {
        public TelegramBotClient BotClient { get; }
        public Message RequestMessage { get; }
        public IMessage ResponseMessage { get; }
        public CancellationToken CancellationToken { get; }

        public BaseResponse(TelegramBotClient botClient, Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken)
        {
            BotClient = botClient;
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
            CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Sends <see cref="ResponseMessage"/>
        /// </summary>
        /// <returns></returns>
        public abstract Task<Message> SendAsync();
        /// <summary>
        /// Edit <see cref="RequestMessage"/>
        /// </summary>
        /// <returns></returns>
        public abstract Task<Message> EditAsync();
    }
}
