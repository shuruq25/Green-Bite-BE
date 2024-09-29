using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> CreateOneAsync(Category newCategory);
        Task<bool> DeleteOnAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(Guid id);
        Task<bool> UpdateOneAsync(Category updatedCategory);
    }

    public class CategoryRepository : ICategoryRepository
    {
        protected DbSet<Category> _category;
        protected DatabaseContext _databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _category = databaseContext.Set<Category>();
        }

        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            await _category.AddAsync(newCategory);
            await _databaseContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category?> GetCategoryAsync(Guid id)
        {
            return await _category.FindAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _category.ToListAsync();
        }

        public async Task<bool> UpdateOneAsync(Category updatedCategory)
        {
            _category.Update(updatedCategory);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOnAsync(Category category)
        {
            _category.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
