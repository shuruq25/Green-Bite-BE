using src.DTO;

namespace src.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO.Get>> GetAllOrdersAsync();
        Task<OrderDTO.Get?> GetOrderByIdAsync(Guid id);
        Task<OrderDTO.Get> CreateOneOrderAsync(OrderDTO.Create orderDTO);
        Task<bool> UpdateOrderAsync(Guid id ,OrderDTO.Update orderDTO);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
