using MentalMathTelegramBot.Controllers.Data;
using MentalMathTelegramBot.Infrastructure.Attributes;
using MentalMathTelegramBot.Infrastructure.Controllers;
using MentalMathTelegramBot.Infrastructure.Messages;

namespace MentalMathTelegramBot.Controllers.Controllers.Tests
{
    [Path("/testRuleSimple")]
    public class TestRuleSimpleMessageController : BaseMessageController
    {
        private readonly BotDbContext dbContext;

        private Dictionary<int, List<int>> allowedNumbers { get; set; } = new Dictionary<int, List<int>>()
        {
            {0, new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } },
            {1, new List<int>() { 0, 1, 2, 3, 5, 6, 7, 8 } },
            {2, new List<int>() { 0, 1, 2, 5, 6, 7 } },
            {3, new List<int>() { 0, 1, 5, 6 } },
            {4, new List<int>() { 0, 5 } },
            {5, new List<int>() { 0, 1, 2, 3, 4 } },
            {6, new List<int>() { 0, 1, 2, 3 } },
            {7, new List<int>() { 0, 1, 2 } },
            {8, new List<int>() { 0, 1 } },
            {9, new List<int>() { 0 } },
        };

        public TestRuleSimpleMessageController(BotDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task DoAction()
        {
            var seq = GetSequence(3);
            int sum = seq.Sum();
            var msg = await SendMessageAsync(new TextMessage($"Элемент 0: {seq[0]}"));
            
            await Task.Delay(700);

            seq.Remove(seq[0]);

            for (int i = 0; i < seq.Count; i++)
            {
                await EditMessageAsync(msg, new TextMessage($"Элемент {i+1}: {seq[i]}"));
                await Task.Delay(500);
            }

            dbContext.Answers.Add(new Data.Models.TestAnswer { UserId = Context.RequestMessage.Chat.Id.ToString(), Answer = sum.ToString(), TestType = Test.SimpleRule });
            dbContext.SaveChanges();

            await SendMessageAsync(new TextMessage($"Сумма: {sum}"));
        }

        private List<int> GetSequence(int count)
        {
            var random = new Random();
            var seq = new List<int>();
            int begin = random.Next(9);

            seq.Add(begin);

            for (int i = 0; i < count - 1; i++)
            {
                int lastNumber = Math.Abs(seq.Sum() % 10);

                if (random.NextSingle() > 0.5)
                {
                    var nums = allowedNumbers[lastNumber];
                    seq.Add(nums[random.Next(nums.Count)]);
                }
                else
                {
                    int index = 8 - lastNumber;
                    if (index < 0) { i--; continue; }
                    var nums = allowedNumbers[index];
                    seq.Add(-nums[random.Next(nums.Count)]);
                }
            }

            return seq;
        }
    }
}
