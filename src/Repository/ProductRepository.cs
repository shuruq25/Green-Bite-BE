using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;
using static src.DTO.ProductDTO;

namespace src.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateOneAsync(Product newProduct);
        Task<bool> DeleteOneAsync(Product product);
        Task<List<Product>> GetAllAsync(PaginationOptions paginationOptions);
        Task<Product?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Product updateProduct);
    }

    public class ProductRepository : IProductRepository
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

        public async Task<List<Product>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var searchResult = _product.Where(c =>
                c.Name.ToLower().Contains(paginationOptions.Search)
            );
            if (paginationOptions.Filter.MinPrice.HasValue)
            {
                searchResult = searchResult.Where(p =>
                    p.Price >= paginationOptions.Filter.MinPrice.Value
                );
            }
            if (paginationOptions.Filter.MaxPrice.HasValue)
            {
                searchResult = searchResult.Where(p =>
                    p.Price <= paginationOptions.Filter.MaxPrice.Value
                );
            }

          
            if (paginationOptions.Sort.SortByPriceAscending)
            {
                searchResult = searchResult.OrderBy(p => p.Price);
            }
            else
            {
                searchResult = searchResult.OrderByDescending(p => p.Price);
            }
            return await searchResult
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
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
