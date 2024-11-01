using Microsoft.AspNetCore.Mvc;
using src.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static src.DTO.MealPlanDTO;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }

        // POST: api/mealplan
        [HttpPost]
        public async Task<ActionResult<MealPlanReadDto>> CreateMealPlanAsync([FromBody] MealPlanCreateDto createDto)
        {
            try
            {
                var result = await _mealPlanService.CreateMealPlanAsync(createDto);
                  return Created($"/api/v1//{result.Id}", result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/mealplan
        [HttpGet]
        public async Task<ActionResult<List<MealPlanReadDto>>> GetAllMealPlansAsync()
        {
            try
            {
                var result = await _mealPlanService.GetAllMealPlansAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/mealplan/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MealPlanReadDto>> GetMealPlanByIdAsync(Guid id)
        {
            try
            {
                var result = await _mealPlanService.GetMealPlanByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Meal plan not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/mealplan/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMealPlanAsync(Guid id, [FromBody] MealPlanUpdateDto updateDto)
        {
            try
            {
                var result = await _mealPlanService.UpdateMealPlanAsync(id, updateDto);
                if (!result)
                {
                    return NotFound(new { message = "Meal plan not found." });
                }
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/mealplan/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMealPlanAsync(Guid id)
        {
            try
            {
                var result = await _mealPlanService.DeleteMealPlanAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Meal plan not found." });
                }
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

