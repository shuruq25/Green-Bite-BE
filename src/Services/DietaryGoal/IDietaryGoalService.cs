using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.DTO;
using static src.DTO.DietaryGoalDTO;

namespace src.Services
{
    public interface IDietaryGoalService
    {
        Task<DietaryGoalReadDto> CreateDietaryGoalAsync(DietaryGoalCreateDto newGoal);
        Task<List<DietaryGoalReadDto>> GetAllDietaryGoalsAsync();
        Task<DietaryGoalReadDto> GetDietaryGoalByIdAsync(Guid id);
        Task<bool> UpdateDietaryGoalAsync(Guid id, DietaryGoalUpdateDto updateDto);
        Task<bool> DeleteDietaryGoalAsync(Guid id);
    }
}
