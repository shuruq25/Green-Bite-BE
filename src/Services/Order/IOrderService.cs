using src.DTO;

namespace src.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO.Get>> GetAllOrdersAsync();
        Task<OrderDTO.Get?> GetOrderByIdAsync(int id);
        Task<OrderDTO.Get> CreateOneOrderAsync(OrderDTO.Create orderDTO);
        Task<bool> UpdateOrderAsync(int id ,OrderDTO.Update orderDTO);
        Task<bool> DeleteByIdAsync(int id);
    }
}
