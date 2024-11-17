using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.DTO;
using src.Repository;
using src.Entity;
using static src.DTO.DietaryGoalDTO; // Make sure to include this

namespace src.Services
{
    public class DietaryGoalService : IDietaryGoalService
    {
        private readonly IDietaryGoalRepository _dietaryGoalRepo;
        private readonly IMapper _mapper;

        public DietaryGoalService(IDietaryGoalRepository dietaryGoalRepo, IMapper mapper )
        {
            _dietaryGoalRepo = dietaryGoalRepo;
            _mapper = mapper;
        }

        public async Task<DietaryGoalReadDto> CreateDietaryGoalAsync(DietaryGoalCreateDto newGoal)
        {
              var dietaryGoal = _mapper.Map<DietaryGoalCreateDto,DietaryGoal>(newGoal);
                var createdGoal = await _dietaryGoalRepo.CreateOneAsync(dietaryGoal);
                return _mapper.Map<DietaryGoal,DietaryGoalReadDto>(createdGoal);
            
        }

        public async Task<List<DietaryGoalReadDto>> GetAllDietaryGoalsAsync()
        {
          
                var goals = await _dietaryGoalRepo.GetAllAsync();
                return _mapper.Map<List<DietaryGoal>,List<DietaryGoalReadDto>>(goals);
           
           
        }

        public async Task<DietaryGoalReadDto> GetDietaryGoalByIdAsync(Guid id)
        {
              var goal = await _dietaryGoalRepo.GetByIdAsync(id);
                if (goal == null)
                {
                    throw new KeyNotFoundException($"Dietary goal with ID {id} not found.");
                }
                return _mapper.Map<DietaryGoal,DietaryGoalReadDto>(goal);
        
        }

        public async Task<bool> UpdateDietaryGoalAsync(Guid id, DietaryGoalUpdateDto updateDto)
        {
           
                var goal = await _dietaryGoalRepo.GetByIdAsync(id);
                if (goal == null)
                {
                    throw new KeyNotFoundException($"Dietary goal with ID {id} not found.");
                }

                _mapper.Map(updateDto, goal);
                return await _dietaryGoalRepo.UpdateOneAsync(goal);
            }
           
        

        public async Task<bool> DeleteDietaryGoalAsync(Guid id)
        {
           
                var goal = await _dietaryGoalRepo.GetByIdAsync(id);
               if (goal != null)
            {
                bool isDeleted = await _dietaryGoalRepo.DeleteOneAsync(goal);
                if (isDeleted)
                {
                    return true;
                }
            }
            return false;
         
        }
    }
}
