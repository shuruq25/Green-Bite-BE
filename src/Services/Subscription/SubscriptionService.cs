using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.Entity;
using src.Repository;
using static src.DTO.SubscriptionDTO;

namespace src.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepo, IMapper mapper)
        {
            _subscriptionRepo = subscriptionRepo;
            _mapper = mapper;
        }

        public async Task<SubscriptionReadDto> CreateSubscriptionAsync(SubscriptionCreateDto newSubscription)
        {
            var subscription = _mapper.Map<SubscriptionCreateDto,Subscription>(newSubscription);

            var createdSubscription = await _subscriptionRepo.CreateOneAsync(subscription);

            return _mapper.Map<SubscriptionReadDto>(createdSubscription);
        }

        public async Task<List<SubscriptionReadDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionRepo.GetAllAsync();
            return _mapper.Map<List<SubscriptionReadDto>>(subscriptions);
        }

        public async Task<SubscriptionReadDto> GetSubscriptionByIdAsync(Guid id)
        {
            var subscription = await _subscriptionRepo.GetByIdAsync(id);
            return _mapper.Map<SubscriptionReadDto>(subscription);
        }

        public async Task<bool> DeleteSubscriptionAsync(Guid subscriptionId)
        {
            var subscription = await _subscriptionRepo.GetByIdAsync(subscriptionId);
            if (subscription == null)
            {
                return false;
            }

            return await _subscriptionRepo.DeleteOneAsync(subscription);
        }

        public async Task<bool> UpdateSubscriptionAsync(Guid id, SubscriptionUpdateDto updateDto)
        {
            var subscription = await _subscriptionRepo.GetByIdAsync(id);
            if (subscription == null)
            {
                return false;
            }

            _mapper.Map(updateDto, subscription);

            return await _subscriptionRepo.UpdateOneAsync(subscription);
        }
    }
}
