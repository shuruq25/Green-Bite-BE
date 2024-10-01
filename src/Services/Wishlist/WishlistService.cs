
using AutoMapper;
using src.Entity;
using src.Repository;
using src.Services;
using static src.DTO.WishlistDTO;

namespace src.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IMapper _mapper;

        public WishlistService(IWishlistRepository wishlistRepo, IMapper mapper)
        {
            _wishlistRepo = wishlistRepo;
            _mapper = mapper;
        }

        public async Task<WishlistReadDto> CreateOneAsync(WishlistCreateDto createDto)
        {
            var wishlist = _mapper.Map<WishlistCreateDto, Wishlist>(createDto);
            var wishlisCreated = await _wishlistRepo.CreateOneAsync(wishlist);
            return _mapper.Map<Wishlist, WishlistReadDto>(wishlisCreated);
        }

        public async Task<List<WishlistReadDto>> GetAllAsync()
        {
            var wishlistItems = await _wishlistRepo.GetAllAsync();
            return _mapper.Map<List<Wishlist>, List<WishlistReadDto>>(wishlistItems);

        }

        public async Task<WishlistReadDto> GetByIdAsync(Guid id)
        {
            var wishlistItem = await _wishlistRepo.GetByIdAsync(id);
            return _mapper.Map<Wishlist, WishlistReadDto>(wishlistItem);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {

            var foundWishlist = await _wishlistRepo.GetByIdAsync(id);
            bool isDeleted = await _wishlistRepo.DeleteOneAsync(foundWishlist);
            if (isDeleted)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, WishlistUpdateDto updateDto)
        {
            var foundWishlistItem = await _wishlistRepo.GetByIdAsync(id);
            if (foundWishlistItem == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundWishlistItem);
            return await _wishlistRepo.UpdateOneAsync(foundWishlistItem);
        }
    }
}
