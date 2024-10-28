using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    
          public interface ISubscriptionRepository
    {
        Task<Subscription> CreateOneAsync(Subscription newSubscription);
        Task<bool> DeleteOneAsync(Subscription subscription);
        Task<List<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Subscription updateSubscription);
    }

public class SubscriptionRepository : ISubscriptionRepository
    {
        protected DbSet<Subscription> _subscription;
        protected DatabaseContext _databaseContext;

        public SubscriptionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _subscription = databaseContext.Set<Subscription>();
        }

        public async Task<Subscription> CreateOneAsync(Subscription newSubscription)
        {
            await _subscription.AddAsync(newSubscription);
            await _databaseContext.SaveChangesAsync();
            return newSubscription;
        }

        public async  Task<bool> DeleteOneAsync(Subscription subscription)
        {
            _subscription.Remove(subscription);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Subscription>> GetAllAsync()
        {
            return await _subscription
                .Include(sub=>sub.User)
                .ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(Guid id)
        {
              return await _subscription
                .Include(sub=>sub.User)
                .FirstOrDefaultAsync(sub => sub.ID == id);
        }

        public async Task<bool> UpdateOneAsync(Subscription updateSubscription)
        {
            _subscription.Update(updateSubscription);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
