using AutoMapper;
using src.DTO;
using src.Entity;
using static src.DTO.AddressDTO;
using static src.DTO.CartDetailsDTO;
using static src.DTO.CartDTO;
using static src.DTO.CategoryDTO;
using static src.DTO.CouponDTO;
using static src.DTO.ProductDTO;
using static src.DTO.ReviewDTO;
using static src.DTO.UserDTO;
using static src.DTO.WishlistDTO;

namespace src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Product Mappings
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Address Mappings
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // User Mappings
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Category Mappings
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Coupon Mappings
            CreateMap<Coupon, CouponReadDto>();
            CreateMap<CouponCreateDto, Coupon>();
            CreateMap<CouponUpdateDto, Coupon>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Review Mappings
            CreateMap<Review, ReviewDTO.ReviewReadDto>();
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Wishlist Mappings

            CreateMap<Wishlist, WishlistReadDto>();
            CreateMap<WishlistCreateDto, Wishlist>();
            CreateMap<WishlistUpdateDto, Wishlist>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Cart Mappings

            CreateMap<Cart, CartReadDto>();
            CreateMap<CartCreateDto, Cart>();
            CreateMap<CartUpdateDto, Cart>()
        .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            // Cart Details Mappings

            CreateMap<CartDetails, CartDetailsReadDto>();
            CreateMap<CartDetailsCreateDto, CartDetails>();
            CreateMap<CartDetailsUpdateDto, CartDetails>()
        .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            // Order Mappings
            CreateMap<Order, OrderDTO.Get>()
            .ForMember(dest => dest.reviews, opt => opt.MapFrom(src => src.Reviews));
            CreateMap<OrderDTO.Create, Order>();
            CreateMap<OrderDTO, Order>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

        }
    }
}





