﻿using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using MentalMathTelegramBot.Infrastructure.Controllers;

namespace MentalMathTelegramBot.Infrastructure
{
    public class Bot
    {
        private IControllerFactory controllerFactory;
        private ILogger? logger;
        private TelegramBotClient botClient;
        private CancellationTokenSource cts = new();
        private ReceiverOptions receiverOptions;

        private string? adminChatId { get; init; }

        const string TG_TOKEN_KEY = "tgBotToken";
        const string TG_ADMINID_KEY = "adminChat";

        public Bot(IConfigurationRoot config, IControllerFactory controllerFactory, ILogger? logger)
        {
            this.controllerFactory = controllerFactory;
            this.logger = logger;
            receiverOptions = new()
            {
                AllowedUpdates = new[] { UpdateType.Message }
            };

            string? BotToken = config[TG_TOKEN_KEY];
            if (string.IsNullOrEmpty(BotToken))
                throw new TokenIsNullException();

            botClient = new TelegramBotClient(BotToken);

            adminChatId = config[TG_ADMINID_KEY];
        }

        public void Start()
        {
            logger?.LogInformation("Bot started...");
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        public async Task<Message> SendMessageAsync(MessageContext context, IMessage message)
        {
            return await ResolveResponseAsync(context.RequestMessage, message, cts.Token);
        }

        public async Task<Message> EditMessageAsyc(Message editingMessage, IMessage newMessage)
        {
            return await ResovleEditingAsync(editingMessage, newMessage, cts.Token);
        }

        /// <summary>
        /// Proccessing of messages from telegram
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            logger?.LogInformation($"Received a '{messageText}' message in chat {message.Chat.Id}.");

            IMessageController messageController = ResolveController(messageText);

            messageController.Context = new MessageContext(this, message);

            await messageController.DoAction();
        }

        /// <summary>
        /// Logs error and stops the bot
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            logger?.LogCritical(ErrorMessage + Environment.NewLine + "Reload the bot...");

            if (!string.IsNullOrEmpty(adminChatId))
                await botClient.SendTextMessageAsync(adminChatId, ErrorMessage);

            cts.Cancel();
        }


        /// <summary>
        /// Resolve controller using <paramref name="path"/> and <see cref="PathAttribute"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IMessageController ResolveController(string path)
        {
            return controllerFactory.ResolveController(path);
        }

        /// <summary>
        /// Resolve controller using <paramref name="type"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IMessageController ResolveController(Type type)
        {
            return controllerFactory.ResolveController(type);
        }

        /// <summary>
        /// Depending on <paramref name="responseMessage"/> sends a message in chat from <paramref name="requestMessage"/>
        /// </summary>
        /// <param name="requestMessage">Message came from telegram</param>
        /// <param name="responseMessage">Response message from resolved controller</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns></returns>
        async Task<Message> ResolveResponseAsync(Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken)
        {
            Message sendedMessage = new Message();
            switch (responseMessage)
            {
                case TextMessage textMessage:
                    sendedMessage = await botClient.SendTextMessageAsync(
                        chatId: requestMessage.Chat.Id,
                        text: textMessage.Text,
                        cancellationToken: cancellationToken);
                    break;
                case PhotoMessage photoMessage:
                    sendedMessage = await botClient.SendPhotoAsync(
                        chatId: requestMessage.Chat.Id,
                        photo: photoMessage.Photo,
                        caption: photoMessage.Text,
                        cancellationToken: cancellationToken);
                    break;
                default:
                    break;
            }

            return sendedMessage;
        }

        async Task<Message> ResovleEditingAsync(Message editingMessage, IMessage newMessage, CancellationToken cancellationToken)
        {
            Message editedMesssage = new Message();
            switch (newMessage)
            {
                case TextMessage textMessage:
                    editedMesssage = await botClient.EditMessageTextAsync(
                        chatId: editingMessage.Chat.Id,
                        messageId: editingMessage.MessageId,
                        text: textMessage.Text,
                        cancellationToken: cancellationToken);
                    break;
                case PhotoMessage photoMessage:
                    //Bug: when editing message does not have media, telegram sents exception
                    InputMediaPhoto inputMediaPhoto = new InputMediaPhoto(new InputMedia(photoMessage.Stream, "photo"));
                    inputMediaPhoto.Caption = photoMessage.Text;

                    editedMesssage = await botClient.EditMessageMediaAsync(
                        chatId: editingMessage.Chat.Id,
                        messageId: editingMessage.MessageId,
                        media: inputMediaPhoto,
                        cancellationToken: cancellationToken);
                    break;
                default:
                    break;
            }

            return editedMesssage;
        }
    }
}
