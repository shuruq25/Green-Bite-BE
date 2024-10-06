using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllAsync(PaginationOptions paginationOptions);
        Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<Product> CreateOneAsync(Product newProduct);
        Task<Product?> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Product product);
        Task<bool> UpdateOneAsync(Product updateProduct);
        Task<List<Product>> SearchProductsAsync(
            PaginationOptions searchOptions,
            PaginationOptions paginationOptions
        );
        Task<List<Product>> GetAllWithSortingAndFilteringAsync(PaginationOptions paginationOptions);
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

        public async Task<List<Product>> GetAllAsync()
        {
            return await _product
                .Include(p => p.Order)
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<List<Product>> GetAllAsync(PaginationOptions paginationOptions)
        {
            return await _product.Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var searchResult = await _product.ToListAsync();
            return searchResult.Where(product => ids.Contains(product.Id)).ToList();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product
                .Include(p => p.Order)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<List<Product>> SearchProductsAsync(
            PaginationOptions searchOptions,
            PaginationOptions paginationOptions
        )
        {
            var query = _product.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchOptions.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchOptions.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.Description))
            {
                query = query.Where(p =>
                    p.Description.ToLower().Contains(searchOptions.Description.ToLower())
                );
            }

            var products = await query
                .Include(p => p.Category)
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetAllWithSortingAndFilteringAsync(
            PaginationOptions paginationOptions
        )
        {
            var query = _product.AsQueryable();

            if (paginationOptions.Filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= paginationOptions.Filter.MinPrice.Value);
            }

            if (paginationOptions.Filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= paginationOptions.Filter.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(paginationOptions.Filter.Category))
            {
                query = query.Where(p =>
                    p.Category.Name.ToLower().Contains(paginationOptions.Filter.Category.ToLower())
                );
            }

            query = paginationOptions.Sort.SortBy.ToLower() switch
            {
                "price" => paginationOptions.Sort.SortDescending ?? false
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),
                "name" => paginationOptions.Sort.SortDescending ?? false
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),
                _ => query.OrderBy(p => p.Name),
            };
            var totalItems = await query.CountAsync();
            var products = await query
                .Include(p => p.Category)
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();
            return products;
        }
    }

}
