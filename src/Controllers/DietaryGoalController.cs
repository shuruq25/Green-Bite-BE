using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Services;
using static src.DTO.DietaryGoalDTO;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DietaryGoalController : ControllerBase
    {
        private readonly IDietaryGoalService _dietaryGoalService;

        public DietaryGoalController(IDietaryGoalService dietaryGoalService)
        {
            _dietaryGoalService = dietaryGoalService;
        }

        // POST: api/v1/dietarygoal
        [HttpPost]
        public async Task<ActionResult<DietaryGoalReadDto>> CreateDietaryGoalAsync([FromBody] DietaryGoalCreateDto createDto)
        {
            try
            {
                var result = await _dietaryGoalService.CreateDietaryGoalAsync(createDto);
                  return Created($"/api/v1//{result.DietaryGoalID}", result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/v1/dietarygoal
        [HttpGet]
        public async Task<ActionResult<List<DietaryGoalReadDto>>> GetAllDietaryGoalsAsync()
        {
            try
            {
                var result = await _dietaryGoalService.GetAllDietaryGoalsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/v1/dietarygoal/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DietaryGoalReadDto>> GetDietaryGoalByIdAsync(Guid id)
        {
            try
            {
                var result = await _dietaryGoalService.GetDietaryGoalByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Dietary goal not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/v1/dietarygoal/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDietaryGoalAsync(Guid id, [FromBody] DietaryGoalUpdateDto updateDto)
        {
            try
            {
                var result = await _dietaryGoalService.UpdateDietaryGoalAsync(id, updateDto);
                if (!result)
                {
                    return NotFound(new { message = "Dietary goal not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/v1/dietarygoal/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDietaryGoalAsync(Guid id)
        {
            try
            {
                var result = await _dietaryGoalService.DeleteDietaryGoalAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Dietary goal not found." });
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
