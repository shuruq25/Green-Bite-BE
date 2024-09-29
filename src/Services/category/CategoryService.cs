using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Repository;
using src.Entity;
using static src.DTO.CategoryDTO;
using src.Utils;

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


        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            var category = _mapper.Map<CategoryCreateDto, Category>(createDto);
            var createdCategory = await _categoryRepo.CreateOneAsync(category);
            return _mapper.Map<Category, CategoryReadDto>(createdCategory);
        }


        public async Task<List<CategoryReadDto>> GetCategoriesAsync(PaginationOptions paginationOptions)
        {
            var allCategory = await _categoryRepo.GetCategoriesAsync();
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(allCategory);
        }


        public async Task<CategoryReadDto> GetCategoryAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetCategoryAsync(id);
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }


        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetCategoryAsync(id);
            if (foundCategory == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);
        }


        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetCategoryAsync(id);
            bool deleteCategory = await _categoryRepo.DeleteOnAsync(foundCategory);
            if (deleteCategory)
            {
                return true;
            }
            return false;
        }
    }
}
