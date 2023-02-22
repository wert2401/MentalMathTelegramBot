namespace MentalMathTelegramBot.Controllers.MessagesData.JsonModels.Interfaces
{
    public interface IQuestion
    {
        public string Answer { get; set; }
        public string? PostAnswer { get; set; }
        public List<string>? AnswerVariants { get; set; }
    }
}
