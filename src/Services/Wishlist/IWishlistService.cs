
using static src.DTO.WishlistDTO;

namespace src.Services
{
 
    public interface IWishlistService
    {
        Task<WishlistReadDto> CreateOneAsync(WishlistCreateDto createDto);
        Task<List<WishlistReadDto>> GetAllAsync();
        Task<WishlistReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, WishlistUpdateDto updateDto);
    }
}

