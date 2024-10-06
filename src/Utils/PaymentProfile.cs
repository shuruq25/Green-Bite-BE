using AutoMapper;
using src.DTO;
using src.Entity;
namespace src.Utils
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<Payment, PaymentDTO.PaymentReadDto>()
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice));
            CreateMap<PaymentDTO.PaymentCreateDto, Payment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PaymentDTO.PaymentUpdateDto, Payment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
