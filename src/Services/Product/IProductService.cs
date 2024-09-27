using static src.DTO.ProductDTO;

namespace src.Services.product
{
    public interface IProductService
    {
        Task<ProductReadDto> CreateOneAsync(ProductCreateDto CreateDto);
        Task<List<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto UpdateDto);
    }
}