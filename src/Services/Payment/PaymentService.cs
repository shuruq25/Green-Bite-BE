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
        private readonly CouponRepository _couponRepo;

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

                // Check if a coupon is provided and valid
                decimal discountAmount = 0;
                if (newPaymentDto.CouponId.HasValue)
                {
                    var coupon = await _couponRepo.GetCouponByIdAsync(newPaymentDto.CouponId.Value);
                    if (coupon == null)
                    {
                        throw CustomException.BadRequest("Coupon does not exist.");
                    }

                    // Additional logic to check coupon validity can be placed here
                    // Example: Check if the coupon is expired or has usage limits
                    if (coupon.Expire < DateTime.UtcNow)
                    {
                        throw CustomException.BadRequest("Coupon has expired.");
                    }

                    // Assuming coupon has a percentage discount, e.g. 10% off
                    discountAmount = coupon.DiscountPercentage / 100 * order.OriginalPrice;
                }

                // Set the final price, applying the coupon discount if applicable
                var finalPrice = order.OriginalPrice - discountAmount;

                // Create the payment object with the final price
                var createdPayment = new Payment
                {
                    Method = newPaymentDto.Method,
                    OrderId = newPaymentDto.OrderId,
                    FinalPrice = finalPrice,  // Apply the calculated final price
                    PaymentDate = DateTime.UtcNow,
                    Status = PaymentStatus.Pending,
                    CouponId = (Guid)newPaymentDto.CouponId  // Set the coupon ID if available
                };

                // Save the payment to the database
                var paymentEntity = await _paymentRepo.CreateOneAsync(createdPayment);

                // Map the entity to a DTO to return
                return _mapper.Map<PaymentDTO.PaymentReadDto>(paymentEntity);
            }
            catch (Exception ex)
            {
                // Log exception details for debugging
                // Consider logging `ex.Message` or the full `ex.ToString()` here
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
