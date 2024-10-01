using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{

    public interface IWishlistRepository
    {
        Task<Wishlist> CreateOneAsync(Wishlist newWishlist);
        Task<bool> DeleteOneAsync(Wishlist wishlist);
        Task<List<Wishlist>> GetAllAsync();
        Task<Wishlist?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Wishlist updateWishlist);
    }
    public class WishlistRepository : IWishlistRepository
    {
        protected DbSet<Wishlist> _wishlist;
        protected DatabaseContext _databaseContext;

        public WishlistRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _wishlist = databaseContext.Set<Wishlist>();
        }

        public async Task<Wishlist> CreateOneAsync(Wishlist newWishlist)
        {
            await _wishlist.AddAsync(newWishlist);
            await _databaseContext.SaveChangesAsync();
            return newWishlist;
        }

        public async Task<bool> DeleteOneAsync(Wishlist wishlist)
        {
            _wishlist.Remove(wishlist);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Wishlist>> GetAllAsync()
        {
            return await _wishlist.ToListAsync();

        }

        public async Task<Wishlist?> GetByIdAsync(Guid id)
        {
            return await _wishlist.FindAsync(id);
        }

        public async Task<bool> UpdateOneAsync(Wishlist updateWishlist)
        {
            _wishlist.Update(updateWishlist);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}