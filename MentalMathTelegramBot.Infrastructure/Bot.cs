using MentalMathTelegramBot.Infrastructure.Exceptions;
using MentalMathTelegramBot.Infrastructure.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;
using MentalMathTelegramBot.Infrastructure.Responses;
using MentalMathTelegramBot.Infrastructure.Updates.Interfaces;
using MentalMathTelegramBot.Infrastructure.Updates;
using MentalMathTelegramBot.Infrastructure.Updates.Handlers;

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

        public DateTime StartedTime { get; private set; }

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
            StartedTime = DateTime.Now.ToUniversalTime();
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
            var response = await CreateResponse(context, message, cts.Token);
            return await response.SendAsync();
        }

        /// <summary>
        /// Edit existing message. Note if you want to edit just a text, then you need to use <see cref="TextMessage"/>, so on with other messages types.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public async Task<Message> EditMessageAsyc(IUpdateContext context, IMessage newMessage)
        {
            var response = await CreateResponse(context, newMessage, cts.Token);
            return await response.EditAsync();
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
            BaseUpdateHandler updateHandler = UpdateHandlersFactory.CreateUpdateHandler(this, update, controllerFactory);

            await updateHandler.Action();
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

            logger?.LogCritical(ErrorMessage + Environment.NewLine + "Restarting the bot...");

            if (!string.IsNullOrEmpty(adminChatId))
                await botClient.SendTextMessageAsync(adminChatId, ErrorMessage);

            cts.Cancel();

            Start();
        }

        /// <summary>
        /// Depending on <paramref name="responseMessage"/> sends a message in chat from <paramref name="requestContext"/>
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="responseMessage">Response message from resolved controller</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns></returns>

        async Task<BaseResponse> CreateResponse(IUpdateContext requestContext, IMessage newMessage, CancellationToken cancellationToken)
        {
            if (requestContext is QueryContext queryContext && (DateTime.Now.ToUniversalTime() - queryContext.Query.Message!.Date).TotalSeconds < 15)
                await botClient.AnswerCallbackQueryAsync(queryContext.Query.Id, cancellationToken: cancellationToken);

            return ResponseFactory.CreateResponse(botClient, requestContext.RequestMessage, newMessage, cancellationToken);
        }
    }
}
