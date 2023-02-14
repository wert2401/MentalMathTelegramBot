using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MentalMathTelegramBot.Infrastructure
{
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
