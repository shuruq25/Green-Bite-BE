using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

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

        // Create a new category
        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            try
            {
                await _category.AddAsync(newCategory);
                await _databaseContext.SaveChangesAsync();
                return newCategory;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Failed to create category: " + ex.Message);
            }
        }

        // Get a category by ID
        public async Task<Category?> GetCategoryAsync(Guid id)
        {
            try
            {
                return await _category.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error fetching category: " + ex.Message);
            }
        }

        // Get all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                return await _category.ToListAsync();
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error fetching categories: " + ex.Message);
            }
        }

        // Update a category
        public async Task<bool> UpdateOneAsync(Category updatedCategory)
        {
            try
            {
                _category.Update(updatedCategory);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error updating category: " + ex.Message);
            }
        }

        // Delete a category
        public async Task<bool> DeleteOnAsync(Category category)
        {
            try
            {
                _category.Remove(category);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error deleting category: " + ex.Message);
            }
        }
    }
}
