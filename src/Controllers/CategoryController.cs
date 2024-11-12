using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Services.category;
using src.Utils;
using static src.DTO.CategoryDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        // GET: /api/v1/category
        // Retrieve all categories with pagination
        // [HttpGet]
        // public async Task<ActionResult<List<CategoryReadDto>>> GetCategories([FromQuery] PaginationOptions paginationOptions)
        // {
        //     try
        //     {
        //         var categories = await _categoryService.GetCategoriesAsync(paginationOptions);
        //         return Ok(categories);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw CustomException.InternalError(ex.Message);
        //     }
        // }

            [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAllAsync()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return Ok(categoryList);
        }


        // GET: /api/v1/category/{id}
        // Retrieve a single category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategory([FromRoute] Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // POST: /api/v1/category
        // Create a new category (Admin only)
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> CreateOne([FromBody] CategoryCreateDto createDto)
        {
            try
            {
                var createdCategory = await _categoryService.CreateOneAsync(createDto);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // DELETE: /api/v1/category/{id}
        // Delete a category by ID (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            try
            {
                var deletedCategory = await _categoryService.DeleteOneAsync(id);
                if (!deletedCategory)
                {
                    throw CustomException.NotFound();
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // PUT: /api/v1/category/{id}
        // Update a category by ID (Admin only)
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> UpdateCategory(
            [FromRoute] Guid id,
            [FromBody] CategoryUpdateDto updateDto
        )
        {
            try
            {
                var updatedCategory = await _categoryService.UpdateOneAsync(id, updateDto);
                if (!updatedCategory)
                {
                    throw CustomException.NotFound();
                }
                return Ok(await _categoryService.GetCategoryAsync(id));
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }
    }
}
