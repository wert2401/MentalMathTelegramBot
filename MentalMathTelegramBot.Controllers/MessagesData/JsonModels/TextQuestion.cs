using MentalMathTelegramBot.Controllers.MessagesData.JsonModels.Interfaces;

namespace MentalMathTelegramBot.Controllers.MessagesData.JsonModels
{
    public class TextQuestion : IText, IQuestion
    {
        public string Text { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string? PostAnswer { get; set; }
        public List<string>? AnswerVariants { get; set; }
    }
}
