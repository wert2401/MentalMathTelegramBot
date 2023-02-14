using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MentalMathTelegramBot.Infrastructure
{
    public class ServiceCollectionBuilder
    {
        public ServiceCollection Services { get; init; }

        public ServiceCollectionBuilder()
        {
            Services = new ServiceCollection();
        }

        public ServiceProvider Build(IConfigurationRoot configuration)
        {
            RegisterControllers(Services);

            Services.AddSingleton<Bot>(p => new Bot(configuration, p.GetRequiredService<IControllerFactory>(), p.GetService<ILogger<Bot>>()));

            return Services.BuildServiceProvider();
        }

        /// <summary>
        /// Registration of <see cref="ControllerFactory"/> and controllers in assembly
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterControllers(ServiceCollection builder)
        {
            Type controllerBaseType = typeof(IMessageController);
            var dataAccess = Assembly.GetExecutingAssembly();

            var controllersTypes = dataAccess.GetTypes().Where(x => x != controllerBaseType && controllerBaseType.IsAssignableFrom(x));

            builder.AddSingleton<IControllerFactory, ControllerFactory>(p => new ControllerFactory(p, controllersTypes));

            foreach (var controllerType in controllersTypes)
                builder.AddTransient(controllerType);
        }
    }
}
