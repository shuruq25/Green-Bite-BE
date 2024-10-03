using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.CartDTO;

namespace src.Services
{
    public interface ICartService
    {
    Task<CartReadDto> CreateOneAsync(Guid userId, CartCreateDto cartCreate);
    Task<CartReadDto> GetCartByUserIdAsync(Guid cartId);
     Task<bool> DeleteCartAsync(Guid id);
        


    }
}