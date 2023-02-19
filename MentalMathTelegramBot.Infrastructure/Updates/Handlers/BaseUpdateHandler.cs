using MentalMathTelegramBot.Infrastructure.Controllers.Interfaces;
using System.Text.RegularExpressions;

namespace MentalMathTelegramBot.Infrastructure.Updates.Handlers
{
    public abstract class BaseUpdateHandler
    {
        protected Bot Bot { get; }
        protected IControllerFactory ControllerFactory { get; }

        protected BaseUpdateHandler(Bot bot, IControllerFactory controllerFactory)
        {
            Bot = bot;
            ControllerFactory = controllerFactory;
        }

        public (string path, Dictionary<string, string>? parameters) GetPathAndParameters(string input)
        {
            Dictionary<string, string>? parameters = new();
            var matches = Regex.Match(input, @"(?<path>\/\w*)(?<params>\?.*)?");

            string path = matches.Groups["path"].Value;
            string paramsString = matches.Groups["params"].Value.Replace("?", "");

            if (string.IsNullOrEmpty(paramsString))
                return (path, null);

            paramsString.Split("&").ToList().ForEach(p =>
            {
                var splittedParam = p.Split("="); 
                parameters.Add(splittedParam[0], splittedParam[1]);
            });

            return (path, parameters);
        }

        public abstract Task Action();
    }
}
