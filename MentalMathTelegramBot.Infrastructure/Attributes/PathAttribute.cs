using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathTelegramBot.Infrastructure.Attributes
{
    /// <summary>
    /// Attribute for declarinng path in bot`s controllers. 
    /// If <see cref="Path"/>="*", this controller will be routed if request message did not contain "/".
    /// </summary>
    public class PathAttribute : Attribute
    {
        public string Path { get; set; }
        public PathAttribute(string path)
        {
            Path = path;
        }
    }
}
