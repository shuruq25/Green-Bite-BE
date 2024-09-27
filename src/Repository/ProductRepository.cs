using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class ProductRepository
    {
        protected DbSet<Product> _product;
        protected DatabaseContext _databaseContext;
        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _product = databaseContext.Set<Product>();
        }
        public async Task<Product> CreateOneAsync(Product newProduct)
        {
            await _product.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }
          public async Task<List<Product>> GetAllAsync()
        {
            return await _product.ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product.FindAsync(id);
        }
        public async Task<bool> UpdateOneAsync(Product updateProduct)
        {
            _product.Update(updateProduct);
            await _databaseContext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteOneAsync(Product product)
        {
            _product.Remove(product);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

    }
}