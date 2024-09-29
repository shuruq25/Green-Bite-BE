using AutoMapper;
using src.Entity;
<<<<<<< HEAD
using static src.DTO.AddressDTO;
=======
using static src.DTO.CategoryDTO;
>>>>>>> f75fd818a84674f349fed6391800595a609a7445
using static src.DTO.ProductDTO;
using static src.DTO.UserDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

<<<<<<< HEAD
            //address
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
             CreateMap<AddressUpdateDto, Address>().
            ForAllMembers(opts=>opts.Condition((src,dest,srcProperty)=>srcProperty !=null));
            // the Condition to make sure all the filde are not empty so it can convert
        }
=======
            //User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
>>>>>>> f75fd818a84674f349fed6391800595a609a7445

            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
        }

    }
<<<<<<< HEAD
=======
}
>>>>>>> f75fd818a84674f349fed6391800595a609a7445
