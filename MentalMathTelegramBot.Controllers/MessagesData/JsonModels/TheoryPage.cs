using MentalMathTelegramBot.Controllers.MessagesData.JsonModels.Interfaces;

namespace MentalMathTelegramBot.Controllers.MessagesData.JsonModels
{
    public class TheoryPage : IText, IPhoto
    {
        public string PhotoFileName { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
