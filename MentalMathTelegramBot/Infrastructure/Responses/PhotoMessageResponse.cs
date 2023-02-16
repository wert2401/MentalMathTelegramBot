using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MentalMathTelegramBot.Infrastructure.Responses
{
    public class PhotoMessageResponse : BaseResponse
    {
        public PhotoMessageResponse(TelegramBotClient botClient, Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken) 
            : base(botClient, requestMessage, responseMessage, cancellationToken)
        {
        }

        public override Task<Message> SendAsync()
        {
            PhotoMessage photoMessage = (PhotoMessage)ResponseMessage;

            return BotClient.SendPhotoAsync(
                        chatId: RequestMessage.Chat.Id,
                        photo: photoMessage.Photo,
                        caption: photoMessage.Text,
                        cancellationToken: CancellationToken);
        }

        public override Task<Message> EditAsync()
        {
            if (RequestMessage.Photo == null)
                throw new MessageDoesNotContainElementException(nameof(RequestMessage.Photo));

            PhotoMessage photoMessage = (PhotoMessage)ResponseMessage;

            InputMediaPhoto inputMediaPhoto = new InputMediaPhoto(new InputMedia(photoMessage.Stream, "photo"));
            inputMediaPhoto.Caption = photoMessage.Text;

            return BotClient.EditMessageMediaAsync(
                chatId: RequestMessage.Chat.Id,
                messageId: RequestMessage.MessageId,
                media: inputMediaPhoto,
                cancellationToken: CancellationToken);
        }
    }
}
