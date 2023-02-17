using System.Runtime.Serialization;

namespace MentalMathTelegramBot.Infrastructure.Exceptions
{
    internal class MessageDoesNotContainElementException : Exception
    {
        public MessageDoesNotContainElementException(string element) : base($"Message that is tried to edit does not have a {element}")
        {
        }
    }
}