using MentalMathTelegramBot.Controllers.Controllers.Tests;

namespace MentalMathTelegramBot.Controllers.Data.Models
{
    public class TestAnswer : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public Test TestType { get; set; }
    }
}
