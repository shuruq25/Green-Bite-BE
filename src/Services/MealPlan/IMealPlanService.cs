using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static src.DTO.MealPlanDTO;

namespace src.Services
{

    public interface IMealPlanService
    {
        Task<MealPlanReadDto> CreateMealPlanAsync( MealPlanCreateDto newMealPlan);
        Task<bool> UpdateMealPlanAsync(Guid id, MealPlanUpdateDto updateMealPlan);
        Task<bool> DeleteMealPlanAsync(Guid mealPlanId);
        Task<List<MealPlanReadDto>> GetAllMealPlansAsync();
        Task<MealPlanReadDto> GetMealPlanByIdAsync(Guid id);

        
    }

}
    