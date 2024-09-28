using AutoMapper;
using src.Repository;
using src.DTO;
using src.Entity;

namespace src.Services
{
    public class OrderService : IOrderService
    {
        protected readonly IMapper _mapper;
        protected readonly IOrderRepository _ordersRepo;

        public OrderService(IMapper mapper, IOrderRepository order)
        {
            _mapper = mapper;
            _ordersRepo = order;
        }

        public async Task<IEnumerable<OrderDTO.Get>> GetAllOrdersAsync()
        {
            return (await _ordersRepo.GetAllOrdersAsync())
                .Select(order => _mapper.Map<OrderDTO.Get>(order));
        }

        public async Task<OrderDTO.Get> CreateOneOrderAsync(OrderDTO.Create orderDTO)
        {
            Order createdOrder = await _ordersRepo.AddOrderAsync(_mapper.Map<Order>(orderDTO));
            return _mapper.Map<OrderDTO.Get>(createdOrder);
        }

        public async Task<OrderDTO.Get?> GetOrderByIdAsync(int id)
        {
            var requestedOrder = await _ordersRepo.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO.Get>(requestedOrder);
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderDTO.Update orderDTO)
        {
            var foundOrder = await _ordersRepo.GetOrderByIdAsync(id);
            if (foundOrder == null)
            {
                return false;
            }
            _mapper.Map(orderDTO, foundOrder);
            return await _ordersRepo.UpdateOrderAsync(foundOrder);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _ordersRepo.DeleteOrderAsync(id);
        }


    }
}
