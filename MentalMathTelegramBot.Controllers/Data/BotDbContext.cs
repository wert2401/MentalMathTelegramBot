using MentalMathTelegramBot.Controllers.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MentalMathTelegramBot.Controllers.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = botDb.db");
        }

        public DbSet<TestAnswer> Answers { get; set; }
    }
}
