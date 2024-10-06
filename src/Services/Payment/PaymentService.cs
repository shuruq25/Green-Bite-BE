using AutoMapper;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.Entity.Payment;
using System.Net;
using static src.DTO.PaymentDTO;

namespace src.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IOrderRepository _orderRepo;
                private readonly CouponRepository _couponRepo ;

        private readonly IMapper _mapper;

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
                Code = payment.Coupon.Code,
                OrderId = payment.OrderId
            });
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


        public async Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto)
        {
            try
            {
                var order = await _orderRepo.GetOrderByIdAsync(newPaymentDto.OrderId);
                if (order == null)
                {
                    throw CustomException.BadRequest("Order does not exist.");
                }

                if (newPaymentDto.CouponId.HasValue)
                {
                    var coupon = await _couponRepo.GetCouponByIdAsync(newPaymentDto.CouponId.Value);
                    if (coupon == null)
                    {
                        throw CustomException.BadRequest("Coupon does not exist.");
                    }
                }
                

                var createdPayment = new Payment
                {
                    Method = newPaymentDto.Method,
                    OrderId = newPaymentDto.OrderId,
                    FinalPrice = newPaymentDto.FinalPrice, 
                    PaymentDate = DateTime.UtcNow,
                    Status = PaymentStatus.Pending,
                    CouponId = (Guid)newPaymentDto.CouponId
                };

                var paymentEntity = await _paymentRepo.CreateOneAsync(createdPayment);

                return _mapper.Map<PaymentDTO.PaymentReadDto>(paymentEntity);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("An error occurred while creating the payment.");
            }
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
