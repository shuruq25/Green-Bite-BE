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
        Task<CartReadDto> CreateOneAsync(Guid id , CartCreateDto cartCreate);
         Task<CartReadDto> GetCartByIdAsync(Guid cartId);
         Task<CartReadDto> UpdateOneAsync(Guid id, CartUpdateDto UpdateDto);

        
        


    }
}