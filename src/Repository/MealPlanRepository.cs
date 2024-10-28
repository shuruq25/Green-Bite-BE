using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
        
    public interface IMealPlanRepository
    {
        Task<MealPlan> CreateOneAsync(MealPlan newMealPlan);
        Task<bool> DeleteOneAsync(MealPlan mealPlan);
        Task<List<MealPlan>> GetAllAsync();
        Task<MealPlan?> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(MealPlan updateMealPlan);
    }

    public class MealPlanRepository : IMealPlanRepository
    {
        protected DbSet<MealPlan> _mealPlan;
        protected DatabaseContext _databaseContext;

        public MealPlanRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _mealPlan = databaseContext.Set<MealPlan>();
        }

        public async Task<MealPlan> CreateOneAsync(MealPlan newMealPlan)
        {
            await _mealPlan.AddAsync(newMealPlan);
            await _databaseContext.SaveChangesAsync();
            return newMealPlan;
        }

        public async Task<bool> DeleteOneAsync(MealPlan mealPlan)
        {
            _mealPlan.Remove(mealPlan);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MealPlan>> GetAllAsync()
        {
            return await _mealPlan
                .Include(mp => mp.MealPlanMeals)
                .ThenInclude(mpm => mpm.Product) 
                .ToListAsync();
        }

        public async Task<MealPlan?> GetByIdAsync(Guid id)
        {
            return await _mealPlan
                .Include(mp => mp.MealPlanMeals)
                .ThenInclude(mpm => mpm.Product)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task<bool> UpdateOneAsync(MealPlan updateMealPlan)
        {
            _mealPlan.Update(updateMealPlan);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}

  