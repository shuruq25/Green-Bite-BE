using AutoMapper;
using src.Entity;
using src.Repository;
using src.Services;
using src.Utils;
using static src.DTO.CartDTO;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public CartService(
            ICartRepository cartRepository,
            IMapper mapper,
            IProductRepository productRepository
        )
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        // to create a cart
        public async Task<CartReadDto> CreateOneAsync(Guid userId, CartCreateDto cartCreate)
        {
            var cart = _mapper.Map<CartCreateDto, Cart>(cartCreate);
            cart.UserId = userId;

            var savedCart = await _cartRepository.CreateOneAsync(cart);

            return _mapper.Map<Cart, CartReadDto>(savedCart);
        }

        // get the cart by user id
        public async Task<CartReadDto> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart for User ID {userId} not found.");
            }

            return _mapper.Map<Cart, CartReadDto>(cart);
        }

        // delete the cart
        public async Task<bool> DeleteCartAsync(Guid userId)
        {
            var existingCart = await _cartRepository.GetByIdAsync(userId);

            if (existingCart == null)
            {
                return false;
            }
            await _cartRepository.RemoveCart(existingCart);
            return true;
        }

        // Update an existing cart
        public async Task<bool> UpdateOneAsync(Guid id, CartUpdateDto UpdateDto)
        {
            var foundCart = await _cartRepository.GetByIdAsync(id);
            if (foundCart == null)
            {
                throw CustomException.NotFound($"Product with ID '{id}' not found.");
            }
            _mapper.Map(UpdateDto, foundCart);
            return await _cartRepository.UpdateOneAsync(foundCart);
        }
    }
}
