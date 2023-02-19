using System.Runtime.Serialization;
using Telegram.Bot.Types.Enums;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    [Serializable]
    internal class UpdateNotHandledException : Exception
    {
        public UpdateNotHandledException(UpdateType type) : base($"Telegram update was not handled. Update type {type}")
        {
        }
    }
}