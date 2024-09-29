using AutoMapper;
using src.Entity;
using static src.DTO.CategoryDTO;
using static src.DTO.ProductDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));



        }
    }
}