using MentalMathTelegramBot.Infrastructure.Exceptions;
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

namespace MentalMathTelegramBot.Infrastructure
{
    public class Bot
    {
        private IControllerFactory controllerFactory;
        private ILogger? logger;
        private TelegramBotClient botClient;
        private CancellationTokenSource cts = new();
        private ReceiverOptions receiverOptions;

        const string TG_TOKEN = "tgBotToken";

        public Bot(IConfigurationRoot config, IControllerFactory controllerFactory, ILogger? logger)
        {
            this.controllerFactory = controllerFactory;
            this.logger = logger;
            receiverOptions = new()
            {
                AllowedUpdates = new[] { UpdateType.Message } // receive all update types
            };

            string? BotToken = config[TG_TOKEN];
            if (string.IsNullOrEmpty(BotToken))
                throw new TokenIsNullException();

            botClient = new TelegramBotClient(BotToken);
        }

        public void Start()
        {
            logger.LogInformation("Bot started...");
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
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

            IMessage responseMessage = ResolveController(messageText).Get();

            await ResolveResponseAsync(message, responseMessage, cancellationToken);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
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
        /// Depending on <paramref name="responseMessage"/> sends a message in chat from <paramref name="requestMessage"/>
        /// </summary>
        /// <param name="requestMessage">Message came from telegram</param>
        /// <param name="responseMessage">Response message from resolved controller</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns></returns>
        async Task ResolveResponseAsync(Message requestMessage, IMessage responseMessage, CancellationToken cancellationToken)
        {
            switch (responseMessage)
            {
                case TextMessage textMessage:
                    await botClient.SendTextMessageAsync(
                        chatId: requestMessage.Chat.Id,
                        text: textMessage.Text,
                        cancellationToken: cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}
