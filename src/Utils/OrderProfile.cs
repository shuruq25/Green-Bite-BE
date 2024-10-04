using AutoMapper;
using src.DTO;
using src.Entity;

namespace src.Utils
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderDTO.Create, Order>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Order, OrderDTO.Get>()
            .ForMember(dest => dest.reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<OrderDTO.Update, Order>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
