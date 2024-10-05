using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
namespace src.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IPaymentRepository _paymentRepo;
        public readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPaymets()
        {
            return (await _paymentRepo.GetAllAsync())
                    .Select(payment => new PaymentDTO.PaymentReadDto
                    {
                        Id = payment.Id,
                        FinalPrice = payment.FinalPrice,
                        Method = payment.Method,
                        PaymentDate = payment.PaymentDate,
                        Status = payment.Status,
                        CouponId = payment.CouponId,
                        Code = payment.Coupon.Code,
                        OrderId = payment.OrderId
                    });
        }

        public async Task<PaymentDTO.PaymentReadDto?> GetPaymentById(Guid id)
        {
            return _mapper.Map<PaymentDTO.PaymentReadDto>(await _paymentRepo.GetByIdAsync(id));
        }

        public async Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto)
        {
            newPaymentDto.PaymentDate = DateTime.UtcNow;
            var createdPyment = await _paymentRepo.CreateOneAsync(_mapper.Map<Payment>(newPaymentDto));
            return _mapper.Map<PaymentDTO.PaymentReadDto>(createdPyment);
        }

        public async Task<bool> UpdatePaymentById(Guid id, PaymentDTO.PaymentUpdateDto updatedPaymentDto)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(id);
            if (foundPayment == null)
            {
                return false;
            }

            _mapper.Map(updatedPaymentDto, foundPayment);
            return await _paymentRepo.UpdatePaymentAsync(foundPayment);
        }

        public async Task<bool> DeletePaymentById(Guid id)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(id);
            if (foundPayment != null)
            {
                return await _paymentRepo.DeletePaymentAsync(foundPayment);
            }

            return false;
        }
    }

}
