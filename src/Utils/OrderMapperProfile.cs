using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Utils
{
    public class OrderMapperProfile
    {
           CreateMap<OrderDTO.Create, Order>()
         .ForMember(dest => dest.ID, opt => opt.Ignore())
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Order, OrderDTO.Get>();

            CreateMap<OrderDTO.Update, Order>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}