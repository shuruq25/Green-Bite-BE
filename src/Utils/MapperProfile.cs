using AutoMapper;
using src.Entity;
using static src.DTO.AddressDTO;
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
            ForAllMembers(opts =>opts.Condition((src, dest, srcProperty)=>srcProperty!=null));


            //address
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
             CreateMap<AddressUpdateDto, Address>().
            ForAllMembers(opts=>opts.Condition((src,dest,srcProperty)=>srcProperty !=null));
            // the Condition to make sure all the filde are not empty so it can convert
        }

        }
    }
