using MentalMathTelegramBot.Infrastructure.Messages.Interfaces;

namespace MentalMathTelegramBot.Infrastructure.Controllers.Interfaces
{
    public interface IMessageController
    {
        public IMessage Get();
    }
}
