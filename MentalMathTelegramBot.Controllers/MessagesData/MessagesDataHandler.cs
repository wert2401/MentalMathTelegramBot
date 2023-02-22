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
        const string theoryTestFileName = "theoryTest.json";
        const string factsTestFileName = "factsTest.json";

        private List<TheoryPage> theoryPages;
        private List<PhotoQuestionQuestion> abacusNumberQuestions;
        private List<TextQuestion> theoryQuestions;
        private List<TextQuestion> factsQuestions;

        public int TheoryPagesCount => theoryPages.Count;

        public MessagesDataHandler()
        {
            theoryPages = JsonSerializer.Deserialize<List<TheoryPage>>(File.ReadAllText($"{folderJsonName}/{theoryFileName}")) ?? new List<TheoryPage>();
            abacusNumberQuestions = JsonSerializer.Deserialize<List<PhotoQuestionQuestion>>(File.ReadAllText($"{folderJsonName}/{abacusNumberTestFileName}")) ?? new List<PhotoQuestionQuestion>();
            theoryQuestions = JsonSerializer.Deserialize<List<TextQuestion>>(File.ReadAllText($"{folderJsonName}/{theoryTestFileName}")) ?? new List<TextQuestion>();
            factsQuestions = JsonSerializer.Deserialize<List<TextQuestion>>(File.ReadAllText($"{folderJsonName}/{factsTestFileName}")) ?? new List<TextQuestion>();

            abacusNumberQuestions.ForEach(a => a.PhotoFileName = $"{folderImagesName}/AbacusNumberTest/{a.PhotoFileName}");
            theoryPages.ForEach(a => a.PhotoFileName = $"{folderImagesName}/Theory/{a.PhotoFileName}");
        }

        public TheoryPage? GetTheoryPage(int page)
        {
            if (page < 0 || page > theoryPages.Count)
                return null;

            return theoryPages[page];
        }

        public PhotoQuestionQuestion GetRandomAbacusNumberQuestion()
        {
            return abacusNumberQuestions[new Random().Next(abacusNumberQuestions.Count)];
        }

        public TextQuestion GetRandomTheoryQuestion()
        {
            return theoryQuestions[new Random().Next(theoryQuestions.Count)];
        }

        public TextQuestion GetRandomFactQuestion()
        {
            return factsQuestions[new Random().Next(factsQuestions.Count)];
        }
    }
}
