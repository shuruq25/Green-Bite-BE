using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public interface ICartRepository
    {
      Task<Cart> CreateOneAsync(Cart cart );


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
        public async Task<Cart> CreateOneAsync(Cart cart ){
            await _carts.AddAsync(cart);
            await _databaseContext.SaveChangesAsync();
            await _carts.Entry(cart).Collection(o=>o.CartDetails).LoadAsync();

            foreach(var details in cart.CartDetails){
                await _databaseContext.Entry(details).Reference(cd=>cd.Product).LoadAsync();
            }
            return cart;

        }


    }
}