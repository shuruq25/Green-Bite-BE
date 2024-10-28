using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using src.DTO;
using src.Repository;
using src.Entity;
using static src.DTO.DietaryGoalDTO;

namespace src.Services
{
    public class DietaryGoalService : IDietaryGoalService
    {
        private readonly IDietaryGoalRepository _dietaryGoalRepo;
        private readonly IMapper _mapper;

        public DietaryGoalService(IDietaryGoalRepository dietaryGoalRepo, IMapper mapper)
        {
            _dietaryGoalRepo = dietaryGoalRepo;
            _mapper = mapper;
        }

        public async Task<DietaryGoalReadDto> CreateDietaryGoalAsync(DietaryGoalCreateDto newGoal)
        {
            var dietaryGoal = _mapper.Map<DietaryGoal>(newGoal);
            var createdGoal = await _dietaryGoalRepo.CreateOneAsync(dietaryGoal);
            return _mapper.Map<DietaryGoalReadDto>(createdGoal);
        }

        public async Task<List<DietaryGoalReadDto>> GetAllDietaryGoalsAsync()
        {
            var goals = await _dietaryGoalRepo.GetAllAsync();
            return _mapper.Map<List<DietaryGoalReadDto>>(goals);
        }

        public async Task<DietaryGoalReadDto> GetDietaryGoalByIdAsync(Guid id)
        {
            var goal = await _dietaryGoalRepo.GetByIdAsync(id);
            return _mapper.Map<DietaryGoalReadDto>(goal);
        }

        public async Task<bool> UpdateDietaryGoalAsync(Guid id, DietaryGoalUpdateDto updateDto)
        {
            var goal = await _dietaryGoalRepo.GetByIdAsync(id);
            if (goal == null)
            {
                return false;
            }

            _mapper.Map(updateDto, goal);
            return await _dietaryGoalRepo.UpdateOneAsync(goal);
        }

        public async Task<bool> DeleteDietaryGoalAsync(Guid id)
        {
            var goal = await _dietaryGoalRepo.GetByIdAsync(id);
            if (goal == null)
            {
                return false;
            }

            return await _dietaryGoalRepo.DeleteOneAsync(goal);
        }
    }
}
