using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.PaymentDTO;

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

        public async Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPayments(int page = 1, int pageSize = 10)
        {
            var payments = await _paymentRepo.GetAllAsync(page, pageSize);
            return payments.Select(payment => new PaymentDTO.PaymentReadDto
            {
                Id = payment.Id,
                FinalPrice = payment.FinalPrice,
                Method = payment.Method,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
                //CouponId = payment.CouponId,
             //   Code = payment.Coupon.Code,
                OrderId = payment.OrderId
            });
        }

        public async Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto)
        {
            if (newPaymentDto.OrderId == null)
            {
                throw CustomException.NotFound();
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
        public async Task<PaymentDTO.PaymentReadDto?> GetPaymentById(Guid id)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(id);
            if (foundPayment == null)
            {
                throw CustomException.NotFound($"Product with ID '{id}' not found.");
            }

            return _mapper.Map<Payment, PaymentReadDto>(foundPayment);
        }

        public async Task<bool> DeletePaymentById(Guid id)
        {
            try
            {
                var foundPayment = await _paymentRepo.GetByIdAsync(id);
                if (foundPayment != null)
                {
                    return await _paymentRepo.DeletePaymentAsync(foundPayment);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("An error occurred while deleting payment.");
            }
        }
    }
}
