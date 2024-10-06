using static src.DTO.CartDTO;

namespace src.Services
{
    public interface ICartService
    {
    Task<CartReadDto> CreateOneAsync(Guid userId, CartCreateDto cartCreate);
    Task<CartReadDto> GetCartByUserIdAsync(Guid cartId);
     Task<bool> DeleteCartAsync(Guid id);
     Task<bool> UpdateOneAsync(Guid id, CartUpdateDto UpdateDto);    


    }
}