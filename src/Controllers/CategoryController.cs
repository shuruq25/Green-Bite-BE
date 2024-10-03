using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.category;
using src.Utils;
using static src.DTO.CategoryDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategories(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var categories = await _categoryService.GetCategoriesAsync(paginationOptions);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategory([FromRoute] Guid id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            return Ok(category);
        }

        [HttpPost]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CategoryReadDto>> CreateOne(
            [FromBody] CategoryCreateDto createDto
        )
        {
            var createdCategory = await _categoryService.CreateOneAsync(createDto);
            return Created($"/api/v1/Category/{createdCategory.Id}", createdCategory);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var deletedCategory = await _categoryService.DeleteOneAsync(id);
            if (!deletedCategory)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> UpdateCategory(
            [FromRoute] Guid id,
            [FromBody] CategoryUpdateDto updateDto
        )
        {
            var updateCategory = await _categoryService.UpdateOneAsync(id, updateDto);
            if (!updateCategory)
            {
                return NotFound();
            }
            var updatedCategory = await _categoryService.GetCategoryAsync(id);
            return Ok(updatedCategory);
        }
    }
}
