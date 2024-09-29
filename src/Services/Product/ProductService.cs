using AutoMapper;
using src.Repository;
using src.Entity;
using static src.DTO.ProductDTO;
using src.Utils;

namespace src.Services.product
{
    public class ProductService : IProductService
    {

        protected readonly IProductRepository _productRepository;
        protected readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }
        public async Task<ProductReadDto> CreateOneAsync(ProductCreateDto CreateDto)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(CreateDto);
            var productCreated = await _productRepository.CreateOneAsync(product);
            return _mapper.Map<Product, ProductReadDto>(productCreated);

        }

        public async Task<List<ProductReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var productList = await _productRepository.GetAllAsync(paginationOptions);
            return _mapper.Map<List<Product>, List<ProductReadDto>>(productList);
        }

        public async Task<ProductReadDto> GetByIdAsync(Guid id)
        {
            var foundProduct = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<Product, ProductReadDto>(foundProduct);

        }
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundProduct = await _productRepository.GetByIdAsync(id);
            if (foundProduct != null)
            {
                bool isDeleted = await _productRepository.DeleteOneAsync(foundProduct);
                if (isDeleted)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto UpdateDto)
        {
            var foundProduct = await _productRepository.GetByIdAsync(id);
            if (foundProduct == null)
            {
                return false;
            }
            _mapper.Map(UpdateDto, foundProduct);
            return await _productRepository.UpdateOneAsync(foundProduct);

        }
    }
}
