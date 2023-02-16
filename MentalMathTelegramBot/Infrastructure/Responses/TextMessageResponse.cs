using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Responses
{
    public class TextMessageResponse : BaseResponse
    {
        public TextMessageResponse(TelegramBotClient botClient, Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken) 
            : base(botClient, requestMessage, responseMessage, cancellationToken)
        {
        }

        public override Task<Message> SendAsync()
        {
            TextMessage textMessage = (TextMessage)ResponseMessage;

            return BotClient.SendTextMessageAsync(
                        chatId: RequestMessage.Chat.Id,
                        text: textMessage.Text,
                        cancellationToken: CancellationToken);
        }

        public override Task<Message> EditAsync()
        {
            TextMessage textMessage = (TextMessage)ResponseMessage;

            if (RequestMessage.Text == null)
                throw new MessageDoesNotContainElementException(nameof(RequestMessage.Text));

            return BotClient.EditMessageTextAsync(
                        chatId: RequestMessage.Chat.Id,
                        messageId: RequestMessage.MessageId,
                        text: textMessage.Text,
                        cancellationToken: CancellationToken);
        }
    }
}
