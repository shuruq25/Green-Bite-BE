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

            var existingCart = await _cartRepository.GetByIdAsync(userId);
            if (existingCart != null)
            {
                throw new InvalidOperationException($"User with ID {userId} already has a cart.");
            }

            var cart = _mapper.Map<CartCreateDto, Cart>(cartCreate);
            cart.UserId = userId;

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

            try
            {
                var newCart = await _cartRepository.CreateOneAsync(cart);
                return _mapper.Map<Cart, CartReadDto>(newCart);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the cart.", ex);
            }
        }

        public async Task<CartReadDto> GetCartByIdAsync(Guid cartId)
        {

            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with ID {cartId} not found.");
            }
            var cartList = _mapper.Map<Cart, CartReadDto>(cart);


            return cartList;
        }

        public async Task<CartReadDto> UpdateOneAsync(Guid id, CartUpdateDto updateDto)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with ID {id} not found.");
            }

            cart.CartDetails.Clear();

            decimal totalPrice = 0;

            foreach (var detail in updateDto.CartDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.ProductId} not found.");
                }

                var cartDetail = new CartDetails
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity
                };

                cart.CartDetails.Add(cartDetail);
                totalPrice += product.Price * detail.Quantity;
            }

            cart.CartTotal = updateDto.CartDetails.Sum(cd => cd.Quantity);
            cart.TotalPrice = totalPrice;

            var updatedCart = await _cartRepository.UpdateOneAsync(cart);

            return _mapper.Map<Cart, CartReadDto>(updatedCart);
        }







    }



}