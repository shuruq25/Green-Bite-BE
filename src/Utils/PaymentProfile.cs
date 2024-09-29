using AutoMapper;
using src.DTO;
using src.Entity;
namespace src.Utils
{
    public class PaymentMappingProfile : Profile 
    {
        public PaymentMappingProfile()
        {
            CreateMap<PaymentDTO.PaymentCreateDto, Payment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Payment, PaymentDTO.PaymentReadDto>();
            CreateMap<PaymentDTO.PaymentUpdateDto, Payment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
