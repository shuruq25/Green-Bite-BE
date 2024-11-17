using src.DTO;
using static src.DTO.OrderDTO;




namespace src.Services

{

    public interface IOrderService

    {

        Task<IEnumerable<OrderDTO.Get>> GetAllOrdersAsync();

        Task<OrderDTO.Get?> GetOrderByIdAsync(Guid id);

        Task<OrderDTO.Get> CreateOneOrderAsync(Guid userID, OrderDTO.Create orderDTO);

        Task<bool> UpdateOrderAsync(Guid id, OrderDTO.Update orderDTO);

        Task<bool> DeleteByIdAsync(Guid id);
        Task<IEnumerable<Get>> GetOrdersByUserIdAsync(Guid userId);


    }

}