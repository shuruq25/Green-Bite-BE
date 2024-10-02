using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public interface ICartRepository
    {
        Task<Cart> CreateOneAsync(Cart cart);
        Task<Cart?> GetByIdAsync(Guid id);
        Task<Cart> UpdateOneAsync(Cart updatedCart);

    }
    public class CartRepository : ICartRepository
    {
        protected readonly DbSet<Cart> _carts;
        protected DatabaseContext _databaseContext;
        public CartRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _carts = _databaseContext.Set<Cart>();
        }
        public async Task<Cart> CreateOneAsync(Cart cart)
        {

            var existingCart = await _carts.FirstOrDefaultAsync(c => c.UserId == cart.UserId);
            if (existingCart != null)
            {
                throw new InvalidOperationException($"User with ID {cart.UserId} already has a cart.");
            }
            await _carts.AddAsync(cart);
            await _databaseContext.SaveChangesAsync();
            await _carts.Entry(cart).Collection(o => o.CartDetails).LoadAsync();

            foreach (var details in cart.CartDetails)
            {
                await _databaseContext.Entry(details).Reference(cd => cd.Product).LoadAsync();
            }
            return cart;

        }
        public async Task<Cart?> GetByIdAsync(Guid userId)
        {
            return await _carts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> UpdateOneAsync(Cart updatedCart)
        {
            

            var existingCart = await _carts.FindAsync(updatedCart.Id);
            if (existingCart == null)
            {
                throw new KeyNotFoundException($"Cart with ID {updatedCart.Id} not found.");
            }

            _databaseContext.Cart.Update(updatedCart);
            await _databaseContext.SaveChangesAsync();
            return updatedCart;


        }









    }
}