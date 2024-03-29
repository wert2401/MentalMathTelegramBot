﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MentalMathTelegramBot.Infrastructure;
using MentalMathTelegramBot.Infrastructure.Extension;
using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Controllers.MessagesData;

namespace MentalMathTelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder()
                # if DEBUG
                .AddUserSecrets<Program>()
                #endif
                .AddJsonFile("appsettings.json")
                .Build();

                BotBuilder botBuilder = new BotBuilder();

                botBuilder.Services.AddLogging(logging => logging.AddConsole());
                botBuilder.Services.RegisterControllers();
                botBuilder.Services.AddDbContext<BotDbContext>();
                botBuilder.Services.AddSingleton<MessagesDataHandler>();
                botBuilder.Services.AddSingleton<UnitOfWork>();

                Bot bot = botBuilder.Build(config);

                bot.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadLine();
        }
    }
}