using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
namespace src.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly IPaymentRepository _paymentRepo;
        public readonly IOrderRepository _orderRepo;
        public readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepo, IOrderRepository orderRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPaymets()
        {
            return (await _paymentRepo.GetAllAsync())
                    .Select(payment => _mapper.Map<PaymentDTO.PaymentReadDto>(payment));
        }

        public async Task<PaymentDTO.PaymentReadDto?> GetPaymentById(Guid id)
        {
            return _mapper.Map<PaymentDTO.PaymentReadDto>(await _paymentRepo.GetByIdAsync(id));
        }

        public async Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto)
        {
            if (newPaymentDto.OrderId == null)
            {
                throw CustomException.BadRequest();
            }
            var updatedOrder = await _orderRepo.GetOrderByIdAsync(newPaymentDto.OrderId);
            if (updatedOrder == null)
            {
                throw CustomException.NotFound();
            }
            if (newPaymentDto.PaidPrice < updatedOrder.OriginalPrice)
            {
                throw CustomException.BadRequest($"not enough money, at least you need {updatedOrder.OriginalPrice}");
            }
            var createdPayment = await _paymentRepo.CreateOneAsync(_mapper.Map<Payment>(newPaymentDto));
            updatedOrder.PaymentID = createdPayment.Id;
            updatedOrder.Status = OrderStatuses.Shipped;
            createdPayment.Status = Payment.PaymentStatus.Completed;

            await _orderRepo.UpdateOrderAsync(updatedOrder);
            var returnValue = _mapper.Map<PaymentDTO.PaymentReadDto>(createdPayment);
            returnValue.FinalPrice = updatedOrder.OriginalPrice - (updatedOrder.OriginalPrice * (createdPayment.Coupon?.DiscountPercentage) ?? updatedOrder.OriginalPrice);
            return returnValue;
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
