using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using MentalMathTelegramBot.Infrastructure.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace MentalMathTelegramBot.Infrastructure.Controllers
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IServiceProvider scope;
        private IEnumerable<Type> controllersTypes;

        public ControllerFactory(IServiceProvider scope, IEnumerable<Type> controllers)
        {
            controllersTypes = controllers;
            this.scope = scope;
        }

        public IMessageController ResolveController(string path)
        {
            var foundType = controllersTypes
                .Where(x =>
                {
                    var atr = (PathAttribute?)x.GetCustomAttribute(typeof(PathAttribute));
                    return atr != null && atr.Path == path;
                })
                .FirstOrDefault();


            if (foundType != null)
            {
                var controller = scope.GetService(foundType);
                if (controller != null)
                    return (IMessageController)controller;
            }

            return (IMessageController)scope.GetRequiredService(typeof(ErrorController));
        }

        public IMessageController ResolveController(Type type)
        {
            var foundType = controllersTypes
                .Where(x => x == type)
                .FirstOrDefault();

            if (foundType != null)
            {
                var controller = scope.GetService(foundType);
                if (controller != null)
                    return (IMessageController)controller;
            }

            throw new ControllerNotFoundException();
        }
    }
}
