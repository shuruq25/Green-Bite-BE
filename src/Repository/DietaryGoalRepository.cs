using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{


    public interface IDietaryGoalRepository
    {
        Task<DietaryGoal> CreateOneAsync(DietaryGoal dietaryGoal);
        Task<List<DietaryGoal>> GetAllAsync();
        Task<DietaryGoal> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(DietaryGoal dietaryGoal);
        Task<bool> DeleteOneAsync(DietaryGoal dietaryGoal);
    }

    public class DietaryGoalRepository : IDietaryGoalRepository
    {
        private readonly DbSet<DietaryGoal> _dietaryGoals;
        private readonly DatabaseContext _databaseContext;

        public DietaryGoalRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dietaryGoals = databaseContext.Set<DietaryGoal>();
        }

        public async Task<DietaryGoal> CreateOneAsync(DietaryGoal dietaryGoal)
        {
            await _dietaryGoals.AddAsync(dietaryGoal);
            await _databaseContext.SaveChangesAsync();
            return dietaryGoal;
        }

        public async Task<bool> DeleteOneAsync(DietaryGoal dietaryGoal)
        {
            _dietaryGoals.Remove(dietaryGoal);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<DietaryGoal>> GetAllAsync()
        {
            return await _dietaryGoals.ToListAsync();
        }

        public async Task<DietaryGoal> GetByIdAsync(Guid id)
        {
            return await _dietaryGoals.FirstOrDefaultAsync(goal => goal.DietaryGoalID == id);
        }

        public async Task<bool> UpdateOneAsync(DietaryGoal dietaryGoal)
        {
            _dietaryGoals.Update(dietaryGoal);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}