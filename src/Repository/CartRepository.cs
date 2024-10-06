using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public interface ICartRepository
    {
        Task<Cart> CreateOneAsync(Cart cart);
        Task<Cart> GetByIdAsync(Guid id);
        Task RemoveCart(Cart cart);
        Task<bool> UpdateOneAsync(Cart updateCart);
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

        // Create a new Cart
        public async Task<Cart> CreateOneAsync(Cart cart)
        {
            var userExists = await _databaseContext.User.AnyAsync(u => u.UserID == cart.UserId);
            if (!userExists)
            {
                throw CustomException.NotFound("User not found.");
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

        // Get Cart by User ID
        public async Task<Cart> GetByIdAsync(Guid id)
        {
            var cart = await _databaseContext
                .Cart.Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.UserId == id);

            if (cart == null)
            {
                throw CustomException.NotFound($"Cart for User ID {id} not found.");
            }
            return cart;
        }

        // Remove a Cart
        public async Task RemoveCart(Cart cart)
        {
            if (cart != null)
            {
                _databaseContext.Cart.Remove(cart);

                await _databaseContext.SaveChangesAsync();
            }
            else
            {
                throw CustomException.NotFound("Cart not found");
            }
        }

        // Update an existing Cart
        public async Task<bool> UpdateOneAsync(Cart updateCart)
        {
            if (updateCart == null)
            {
                throw CustomException.BadRequest("Cart data is null.");
            }

            // Ensure the cart exists in the database
            var existingCart = await _databaseContext.Cart.FirstOrDefaultAsync(c =>
                c.UserId == updateCart.UserId
            );

            if (existingCart == null)
            {
                throw CustomException.NotFound($"Cart for User ID {updateCart.UserId} not found.");
            }

            _carts.Update(updateCart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
