using AutoMapper;
using src.Entity;
using src.Repository;
using static src.DTO.MealPlanDTO;


namespace src.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IMealPlanRepository _mealPlanRepo;
        private readonly IMapper _mapper;

        public MealPlanService(IMealPlanRepository mealPlanRepo, IMapper mapper)
        {
            _mealPlanRepo = mealPlanRepo;
            _mapper = mapper;
        }

        // Create a new meal plan
        public async Task<MealPlanReadDto> CreateMealPlanAsync( MealPlanCreateDto createDto)
        {
            var mealPlan = _mapper.Map<MealPlanCreateDto, MealPlan>(createDto);

            var createdMealPlan = await _mealPlanRepo.CreateOneAsync(mealPlan);

            return _mapper.Map<MealPlan, MealPlanReadDto>(createdMealPlan);
        }

        // Get all meal plans
        public async Task<List<MealPlanReadDto>> GetAllMealPlansAsync()
        {
            var mealPlans = await _mealPlanRepo.GetAllAsync();
            return _mapper.Map<List<MealPlan>, List<MealPlanReadDto>>(mealPlans);
        }

        // Get meal plan by ID
        public async Task<MealPlanReadDto> GetMealPlanByIdAsync(Guid id)
        {
            var mealPlan = await _mealPlanRepo.GetByIdAsync(id);
            return _mapper.Map<MealPlan, MealPlanReadDto>(mealPlan);
        }

        // Delete a meal plan
        public async Task<bool> DeleteMealPlanAsync(Guid mealPlanId)
        {
            var mealPlan = await _mealPlanRepo.GetByIdAsync(mealPlanId);
            if (mealPlan == null)
            {
                return false;
            }

            return await _mealPlanRepo.DeleteOneAsync(mealPlan);
        }

        // Update a meal plan
        public async Task<bool> UpdateMealPlanAsync(Guid id, MealPlanUpdateDto updateDto)
        {
            var mealPlan = await _mealPlanRepo.GetByIdAsync(id);
            if (mealPlan == null)
            {
                return false;
            }

            _mapper.Map(updateDto, mealPlan);

            return await _mealPlanRepo.UpdateOneAsync(mealPlan);
        }
    }
}
