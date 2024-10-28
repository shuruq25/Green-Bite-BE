using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static src.DTO.SubscriptionDTO;

namespace src.Services
{
    public interface ISubscriptionService
    {
        
        Task<SubscriptionReadDto> CreateSubscriptionAsync( SubscriptionCreateDto newSubscription);
        Task<bool> UpdateSubscriptionAsync(Guid id, SubscriptionUpdateDto updateSubscription);
        Task<bool> DeleteSubscriptionAsync(Guid subscriptionId);
        Task<List<SubscriptionReadDto>> GetAllSubscriptionsAsync();
        Task<SubscriptionReadDto> GetSubscriptionByIdAsync(Guid id);

        
    }
}