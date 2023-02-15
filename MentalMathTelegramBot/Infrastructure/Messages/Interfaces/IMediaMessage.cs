using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Messages.Interfaces
{
    public interface IMediaMessage : IMessage
    {
        public Stream Stream { get; set; }
    }
}
