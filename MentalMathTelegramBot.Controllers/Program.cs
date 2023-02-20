using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MentalMathTelegramBot.Infrastructure;
using MentalMathTelegramBot.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using MentalMathTelegramBot.Controllers.Data;

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
            botBuilder.Services.RegisterControllers();
            botBuilder.Services.AddDbContext<BotDbContext>();

            Bot bot = botBuilder.Build(config);

            bot.Start();

            Console.ReadLine();
        }
    }
}