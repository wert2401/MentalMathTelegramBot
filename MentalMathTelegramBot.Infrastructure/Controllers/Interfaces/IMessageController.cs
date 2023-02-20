using MentalMathTelegramBot.Infrastructure.Updates.Interfaces;

namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IMessageController
    {
        public IUpdateContext Context { get; set; }
        public Task DoAction();
        public Task DoAction(Dictionary<string, string> parameters);
    }
}
