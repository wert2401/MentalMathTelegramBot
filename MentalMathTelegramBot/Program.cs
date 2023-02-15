using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MentalMathTelegramBot.Infrastructure;

namespace MentalMathTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            BotBuilder botBuilder = new BotBuilder();

            botBuilder.Services.AddLogging(logging => logging.AddConsole());

            Bot bot = botBuilder.Build(config);

            bot.Start();

            Console.ReadLine();
        }
    }

    /*  TODO:
     *  - Add InlineKeyboards
     *  - Fix bug with editind media message
     */
}