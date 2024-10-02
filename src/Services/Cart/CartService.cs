using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration.UserSecrets;
using src.Entity;
using src.Repository;
using src.Services;
using static src.DTO.CartDTO;

namespace Services
{
    public class CartService : ICartService
    {
        protected readonly ICartRepository _cartRepository;
        protected readonly IProductRepository _productRepository;

        protected readonly IMapper _mapper;
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


    }


}