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
using MentalMathTelegramBot.Infrastructure.Messages.Queries;
using MentalMathTelegramBot.Infrastructure.Responses;
using Telegram.Bot.Requests;

namespace MentalMathTelegramBot.Infrastructure
{
    public class Bot
    {
        private IControllerFactory controllerFactory;
        private ILogger? logger;
        private TelegramBotClient botClient;
        private CancellationTokenSource cts = new();
        private ReceiverOptions receiverOptions;

        private DateTime startedTime;

        private string? adminChatId { get; init; }

        const string TG_TOKEN_KEY = "tgBotToken";
        const string TG_ADMINID_KEY = "adminChat";

        public Bot(IConfigurationRoot config, IControllerFactory controllerFactory, ILogger? logger)
        {
            this.controllerFactory = controllerFactory;
            this.logger = logger;
            receiverOptions = new()
            {
                AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery }
            };

            string? BotToken = config[TG_TOKEN_KEY];
            if (string.IsNullOrEmpty(BotToken))
                throw new TokenIsNullException();

            botClient = new TelegramBotClient(BotToken);

            adminChatId = config[TG_ADMINID_KEY];
        }

        public void Start()
        {
            cts = new CancellationTokenSource();
            startedTime = DateTime.Now.ToUniversalTime();
            logger?.LogInformation("Bot started...");
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        /// <summary>
        /// Sends a new message.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Message> SendMessageAsync(IUpdateContext context, IMessage message)
        {
            return await ResolveResponseAsync(context, message, cts.Token);
        }

        /// <summary>
        /// Edit existing message. Note if you want to edit just a text, then you need to use <see cref="TextMessage"/>, so on with other messages types.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public async Task<Message> EditMessageAsyc(IUpdateContext context, IMessage newMessage)
        {
            return await ResovleEditingAsync(context, newMessage, cts.Token);
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
            if (update.Message is { } message && message.Date >= startedTime)
                await HandleMessageUpdateAsync(message, cancellationToken);

            if (update.CallbackQuery is { } query && query.Message?.Date >= startedTime)
                await HandleQueryUpdateAsync(query, cancellationToken);
        }

        async Task HandleMessageUpdateAsync(Message message, CancellationToken cancellationToken)
        {
            if (message.Text is not { } messageText)
                return;

            logger?.LogInformation($"Received a '{messageText}' message in chat {message.Chat.Id}.");

            IMessageController messageController = ResolveController(messageText);

            messageController.Context = new MessageContext(this, message);

            await messageController.DoAction();
        }

        async Task HandleQueryUpdateAsync(CallbackQuery query, CancellationToken cancellationToken)
        {
            if (query.Data is not { } messageText)
                return;

            logger?.LogInformation($"Received a '{messageText}' query in chat {query.Message?.Chat.Id}.");

            IMessageController messageController = ResolveController(messageText);

            messageController.Context = new QueryContext(this, query, query.Message);

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

            Start();
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
        /// Depending on <paramref name="responseMessage"/> sends a message in chat from <paramref name="requestContext"/>
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="responseMessage">Response message from resolved controller</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns></returns>
        async Task<Message> ResolveResponseAsync(IUpdateContext requestContext, IMessage responseMessage, CancellationToken cancellationToken)
        {
            var response = ResponseFactory.CreateResponse(botClient, requestContext.RequestMessage, responseMessage, cancellationToken);

            if (requestContext is QueryContext queryContext && (DateTime.Now.ToUniversalTime() - queryContext.RequestMessage.Date).TotalSeconds < 15)
                await botClient.AnswerCallbackQueryAsync(queryContext.Query.Id, cancellationToken: cancellationToken);

            return await response.SendAsync();
        }

        /// <summary>
        /// Depending on <paramref name="newMessage"/> edit the old message in chat from <paramref name="requestContext"/>
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="newMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task<Message> ResovleEditingAsync(IUpdateContext requestContext, IMessage newMessage, CancellationToken cancellationToken)
        {
            var response = ResponseFactory.CreateResponse(botClient, requestContext.RequestMessage, newMessage, cancellationToken);

            if (requestContext is QueryContext queryContext && (DateTime.Now.ToUniversalTime() - queryContext.RequestMessage.Date).TotalSeconds < 15)
                await botClient.AnswerCallbackQueryAsync(queryContext.Query.Id, cancellationToken: cancellationToken);

            return await response.EditAsync();
        }
    }
}
