using MentalMathTelegramBot.Infrastructure.Messages;
using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IMessageController
    {
        public IUpdateContext Context { get; set; }
        public Task DoAction();
    }
}
