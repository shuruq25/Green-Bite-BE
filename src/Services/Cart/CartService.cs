using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Database;
using src.Entity;
using src.Repository;
using src.Services;
using static src.DTO.CartDetailsDTO;
using static src.DTO.CartDTO;

namespace Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;


    public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
         public async Task<CartReadDto> CreateOneAsync(Guid userId, CartCreateDto cartCreate)
        {
            // Validate input
            if (cartCreate == null)
            {
                throw new ArgumentNullException(nameof(cartCreate), "CartCreateDto cannot be null.");
            }

            if (cartCreate.CartDetails == null || !cartCreate.CartDetails.Any())
            {
                throw new ArgumentException("Cart details cannot be empty.", nameof(cartCreate.CartDetails));
            }

            var cart = _mapper.Map<CartCreateDto, Cart>(cartCreate);
            cart.UserId = userId;

            // Initialize total price and cart total
            cart.CartTotal = cartCreate.CartDetails.Sum(cd => cd.Quantity);
            cart.TotalPrice = 0;

            foreach (var detail in cartCreate.CartDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.ProductId} not found.");
                }

                cart.TotalPrice += product.Price * detail.Quantity;
            }

            await _cartRepository.CreateOneAsync(cart);
            return _mapper.Map<Cart, CartReadDto>(cart);
        }


    



        public async Task<CartReadDto> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _cartRepository.GetByIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart for User ID {userId} not found.");
            }

            return _mapper.Map<Cart, CartReadDto>(cart);
        }


        public async Task<bool> DeleteCartAsync(Guid userId)
        {
            // Fetch the cart for the user
            var existingCart = await _cartRepository.GetByIdAsync(userId);

            // Check if the user has a cart
            if (existingCart == null)
            {
                // If no cart is found for the user, return false
                return false;
            }

            // Call the repository to remove the cart and its details
            await _cartRepository.RemoveCart(existingCart);

            // Return true if the cart was successfully deleted
            return true;
        }

    }}
