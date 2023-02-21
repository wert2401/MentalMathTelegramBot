using MentalMathTelegramBot.Controllers.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MentalMathTelegramBot.Controllers.Data
{
    public class UnitOfWork : IDisposable
    {
        BotDbContext appDbContext;

        public UnitOfWork(BotDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public void Add<EntityType>(EntityType entity) where EntityType : BaseEntity
        {
            appDbContext.Set<EntityType>().Add(entity);
            appDbContext.SaveChanges();
        }

        public void Update<EntityType>(EntityType entity) where EntityType : BaseEntity
        {
            appDbContext.Set<EntityType>().Update(entity);
            appDbContext.SaveChanges();
        }

        public void Remove<EntityType>(EntityType entity) where EntityType : BaseEntity
        {
            appDbContext.Set<EntityType>().Remove(entity);
            appDbContext.SaveChanges();
        }

        public async Task RemoveByIdAsync<EntityType>(int id) where EntityType : BaseEntity
        {
            await appDbContext.Set<EntityType>().Where(x => x.Id == id).ExecuteDeleteAsync();
            appDbContext.SaveChanges();
        }

        public EntityType? Get<EntityType>(int id) where EntityType : BaseEntity
        {
            return appDbContext.Set<EntityType>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<EntityType> GetAll<EntityType>() where EntityType : BaseEntity
        {
            return appDbContext.Set<EntityType>().ToList();
        }

        public IEnumerable<EntityType> GetAll<EntityType>(Func<EntityType, bool> func) where EntityType : BaseEntity
        {
            return appDbContext.Set<EntityType>().Where(func).ToList();
        }

        public int Count<EntityType>() where EntityType : BaseEntity
        {
            return appDbContext.Set<EntityType>().Count();
        }

        public void Dispose()
        {
            appDbContext.Dispose();
        }
    }
}
