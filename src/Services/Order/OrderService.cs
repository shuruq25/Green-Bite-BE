using AutoMapper;
using src.Repository;
using src.DTO;
using src.Entity;
using src.Utils;

namespace src.Services
{
    public class OrderService : IOrderService
    {
        protected readonly IMapper _mapper;
        protected readonly IOrderRepository _ordersRepo;
        protected readonly IProductRepository _productRepo;

        public OrderService(IMapper mapper, IOrderRepository ordersRepo, IProductRepository productRepo)
        {
            _mapper = mapper;
            _ordersRepo = ordersRepo;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<OrderDTO.Get>> GetAllOrdersAsync()
        {
            return (await _ordersRepo.GetAllOrdersAsync())
                .Select(order => _mapper.Map<OrderDTO.Get>(order));
        }

        public async Task<OrderDTO.Get> CreateOneOrderAsync(OrderDTO.Create orderDTO)
        {
            var prods = orderDTO.Products;
            if (prods.Count == 0)
            {
                throw CustomException.BadRequest("order must have at least one product");
            }
            orderDTO.Products = null;
            Order createdOrder = await _ordersRepo.AddOrderAsync(_mapper.Map<Order>(orderDTO));
            foreach (var p in prods)
            {
                await AddProductToOrder(createdOrder, p.Id);
            }
            return _mapper.Map<OrderDTO.Get>(createdOrder);
        }

        public async Task AddProductToOrder(Order order, Guid productId)
        {
            var prod = await _productRepo.GetByIdAsync(productId);
            if (prod == null)
            {
                throw CustomException.NotFound();
            }
            order.Products.Add(prod);
            await _ordersRepo.UpdateOrderAsync(order);
        }
        public async Task<OrderDTO.Get?> GetOrderByIdAsync(Guid id)
        {
            var requestedOrder = await _ordersRepo.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO.Get>(requestedOrder);
        }

        public async Task<bool> UpdateOrderAsync(Guid id, OrderDTO.Update orderDTO)
        {
            var foundOrder = await _ordersRepo.GetOrderByIdAsync(id);
            if (foundOrder == null)
            {
                return false;
            }
            _mapper.Map(orderDTO, foundOrder);
            return await _ordersRepo.UpdateOrderAsync(foundOrder);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _ordersRepo.DeleteOrderAsync(id);
        }


    }
}
