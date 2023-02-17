using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Responses
{
    public class QueryMessageKeyboardResponse : BaseResponse
    {
        public QueryMessageKeyboardResponse(TelegramBotClient botClient, Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken) 
            : base(botClient, requestMessage, responseMessage, cancellationToken)
        {
        }

        public override Task<Message> SendAsync()
        {
            QueryMessageKeyboard keyboardMessage = (QueryMessageKeyboard)ResponseMessage;

            return BotClient.SendTextMessageAsync(
                        chatId: RequestMessage.Chat.Id,
                        text: keyboardMessage.Text,
                        replyMarkup: keyboardMessage.ToMarkup(),
                        cancellationToken: CancellationToken);
        }

        public override Task<Message> EditAsync()
        {
            if (RequestMessage.ReplyMarkup == null)
                throw new MessageDoesNotContainElementException(nameof(RequestMessage.ReplyMarkup));

            QueryMessageKeyboard keyboardMessage = (QueryMessageKeyboard)ResponseMessage;

            return BotClient.EditMessageReplyMarkupAsync(
                        chatId: RequestMessage.Chat.Id,
                        messageId: RequestMessage.MessageId,
                        replyMarkup: keyboardMessage.ToMarkup(),
                        cancellationToken: CancellationToken);
        }
    }
}
