using src.Utils;
using static src.DTO.CategoryDTO;

namespace src.Services.category
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);
        Task<List<CategoryReadDto>> GetCategoriesAsync(PaginationOptions paginationOptions);
        Task<CategoryReadDto> GetCategoryAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto);
        Task<bool> DeleteOneAsync(Guid id);
    }
}
