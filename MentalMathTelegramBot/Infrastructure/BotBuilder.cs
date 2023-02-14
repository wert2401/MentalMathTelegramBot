using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;

namespace MentalMathTelegramBot.Infrastructure
{
    /// <summary>
    /// Builds bot, includes all controllers with <see cref="IMessageController"/>. Has <see cref="ServiceCollection"/> for DI.
    /// </summary>
    public class BotBuilder
    {
        public ServiceCollection Services { get => serviceCollectionBuilder.Services; }

        ServiceCollectionBuilder serviceCollectionBuilder { get; init; }

        public BotBuilder()
        {
            serviceCollectionBuilder = new ServiceCollectionBuilder();
        }

        public Bot Build(IConfigurationRoot configuration)
        {
            var container = serviceCollectionBuilder.Build(configuration);

            return container.GetRequiredService<Bot>();
        }
    }
}
