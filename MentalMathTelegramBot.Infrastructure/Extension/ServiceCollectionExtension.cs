using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MentalMathTelegramBot.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterControllers(this ServiceCollection services)
        {
            Type controllerBaseType = typeof(IMessageController);
            var callingAssembly = Assembly.GetCallingAssembly();
            var currentAssembly = Assembly.GetExecutingAssembly();

            var controllersTypes = callingAssembly.GetTypes().Where(x => x != controllerBaseType && controllerBaseType.IsAssignableFrom(x) && !x.IsAbstract);

            controllersTypes = controllersTypes.Concat(currentAssembly.GetTypes().Where(x => x != controllerBaseType && controllerBaseType.IsAssignableFrom(x) && !x.IsAbstract));

            services.AddSingleton<IControllerFactory, ControllerFactory>(p => new ControllerFactory(p, controllersTypes));

            foreach (var controllerType in controllersTypes)
                services.AddTransient(controllerType);
        }
    }
}
