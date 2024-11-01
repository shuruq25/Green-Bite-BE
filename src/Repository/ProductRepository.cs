using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateOneAsync(Product newProduct);
        Task<bool> DeleteOneAsync(Product product);
        Task<List<Product>> GetAllAsync(PaginationOptions paginationOptions);
        Task<Product?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Product updateProduct);
        Task<int> CountAsync();
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

        public async Task<List<Product>> GetAllAsync(PaginationOptions options)
        {
            var productsQuery = _databaseContext.Product
                .Include(p => p.Category) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(options.Search))
            {
                string searchTerm = options.Search.Trim();
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }
            if (options.Filter.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= options.Filter.MinPrice.Value);
            }

            if (options.Filter.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= options.Filter.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(options.Filter.Category?.Name))
            {
                var searchTerm = options.Filter.Category.Name.ToLower(); 
                productsQuery = productsQuery.Where(p => p.Category != null &&
                                                         p.Category.Name.ToLower().Contains(searchTerm));
            }

            productsQuery = productsQuery.Skip(options.Offset).Take(options.Limit);


            return await productsQuery.ToListAsync();
        }





        public async Task<int> CountAsync()
        {
            return await _databaseContext.Set<Product>().CountAsync();
        }


        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _product
                .Include(p => p.Category)
                .Include(p => p.Reviews)
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

            if (!string.IsNullOrWhiteSpace(paginationOptions.Filter.Category.Name))
            {
                var categoryFilter = paginationOptions.Filter.Category.Name.ToLower();
                query = query.Where(p => p.Category.Name.ToLower().Contains(categoryFilter));
            }

            query = paginationOptions.Sort.SortBy.ToLower() switch
            {
                "price" => (paginationOptions.Sort.SortDescending ?? false)
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),

                "name" => (paginationOptions.Sort.SortDescending ?? false)
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),

                _ => query.OrderBy(p => p.Name),
            };

            var totalItems = await query.CountAsync();

            var products = await query
                .Include(p => p.Category)
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

    }
}
