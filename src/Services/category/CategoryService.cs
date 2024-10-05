using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.CategoryDTO;

namespace src.Services.category
{
    public class CategoryService : ICategoryService
    {
        protected ICategoryRepository _categoryRepo;
        protected IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // Create a new category
        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            try
            {
                var category = _mapper.Map<CategoryCreateDto, Category>(createDto);
                var createdCategory = await _categoryRepo.CreateOneAsync(category);
                return _mapper.Map<Category, CategoryReadDto>(createdCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // Get all categories
        public async Task<List<CategoryReadDto>> GetCategoriesAsync(
            PaginationOptions paginationOptions
        )
        {
            try
            {
                var allCategory = await _categoryRepo.GetCategoriesAsync();
                return _mapper.Map<List<Category>, List<CategoryReadDto>>(allCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // Get a category by ID
        public async Task<CategoryReadDto> GetCategoryAsync(Guid id)
        {
            try
            {
                var foundCategory = await _categoryRepo.GetCategoryAsync(id);
                if (foundCategory == null)
                {
                    throw CustomException.NotFound($"Category with {id} cant find");
                }
                return _mapper.Map<Category, CategoryReadDto>(foundCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // Update a category
        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            try
            {
                var foundCategory = await _categoryRepo.GetCategoryAsync(id);
                if (foundCategory == null)
                {
                    throw CustomException.NotFound($"Category with ID '{id}' not found.");
                }

                _mapper.Map(updateDto, foundCategory);
                return await _categoryRepo.UpdateOneAsync(foundCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // Delete a category
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            try
            {
                var foundCategory = await _categoryRepo.GetCategoryAsync(id);
                if (foundCategory == null)
                {
                    throw CustomException.NotFound($"Category with ID '{id}' not found.");
                }
                return await _categoryRepo.DeleteOnAsync(foundCategory);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }
    }
}
