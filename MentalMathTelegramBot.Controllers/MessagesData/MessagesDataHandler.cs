using System.Text.Json;
using MentalMathTelegramBot.Controllers.MessagesData.JsonModels;

namespace MentalMathTelegramBot.Controllers.MessagesData
{
    public class MessagesDataHandler
    {
        const string folderJsonName = "MessagesData/Json";
        const string folderImagesName = "MessagesData/Images";
        const string theoryFileName = "theory.json";
        const string abacusNumberTestFileName = "abacusNumbersTest.json";

        private List<string> theoryPages;
        private List<AbacusNumberQuestion> abacusNumberQuestions;

        public int TheoryPagesCount => theoryPages.Count;

        public MessagesDataHandler()
        {
            theoryPages = JsonSerializer.Deserialize<List<string>>(File.ReadAllText($"{folderJsonName}/{theoryFileName}")) ?? new List<string>();
            abacusNumberQuestions = JsonSerializer.Deserialize<List<AbacusNumberQuestion>>(File.ReadAllText($"{folderJsonName}/{abacusNumberTestFileName}")) ?? new List<AbacusNumberQuestion>();

            abacusNumberQuestions.ForEach(a => a.FileName = $"{folderImagesName}/{a.FileName}");
        }

        public string? GetTheoryPage(int page)
        {
            if (page < 0 || page > theoryPages.Count)
                return null;

            return theoryPages[page];
        }

        public AbacusNumberQuestion GetRandomAbacusNumberQuestion()
        {
            return abacusNumberQuestions[new Random().Next(abacusNumberQuestions.Count)];
        }
    }
}
