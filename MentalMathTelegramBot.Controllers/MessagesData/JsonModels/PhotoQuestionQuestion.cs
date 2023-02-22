using MentalMathTelegramBot.Controllers.MessagesData.JsonModels.Interfaces;

namespace MentalMathTelegramBot.Controllers.MessagesData.JsonModels
{
    public class PhotoQuestionQuestion : IPhoto, IQuestion
    {
        public string Answer { get; set; } = null!;
        public string PhotoFileName { get; set; } = null!;
        public string? PostAnswer { get; set; }
        public List<string>? AnswerVariants { get; set; }
    }
}
