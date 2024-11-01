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





        public async Task<List<DietaryGoal>> GetAllAsync()
        {
            return await _dietaryGoals.ToListAsync();
        }

        public async Task<DietaryGoal> GetByIdAsync(Guid id)
        {
            return await _dietaryGoals.FirstOrDefaultAsync(goal => goal.DietaryGoalID == id);
        }

        public async Task<DietaryGoal> CreateOneAsync(DietaryGoal dietaryGoal)
        {
            try
            {
                await _dietaryGoals.AddAsync(dietaryGoal);
                await _databaseContext.SaveChangesAsync();
                return dietaryGoal;
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                Console.WriteLine($"Error creating dietary goal: {ex.Message}");
                throw; // Rethrow the exception if needed
            }
        }

        public async Task<bool> DeleteOneAsync(DietaryGoal dietaryGoal)
        {
            try
            {
                var existingGoal = await GetByIdAsync(dietaryGoal.DietaryGoalID);
                if (existingGoal == null)
                {
                    return false; // Not found
                }

                _dietaryGoals.Remove(existingGoal);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting dietary goal: {ex.Message}");
                throw; // Rethrow the exception if needed
            }
        }

        public async Task<bool> UpdateOneAsync(DietaryGoal dietaryGoal)
        {
            try
            {
                var existingGoal = await GetByIdAsync(dietaryGoal.DietaryGoalID);
                if (existingGoal == null)
                {
                    return false; // Not found
                }

                _databaseContext.Entry(existingGoal).CurrentValues.SetValues(dietaryGoal);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating dietary goal: {ex.Message}");
                throw; // Rethrow the exception if needed
            }
        }

    }
}