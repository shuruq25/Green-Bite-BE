using src.DTO;
namespace src.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPaymets();
        Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto);
        Task<PaymentDTO.PaymentReadDto?> GetPaymentById(Guid id);
        Task<bool> DeletePaymentById(Guid id);
        Task<bool> UpdatePaymentById(Guid id, PaymentDTO.PaymentUpdateDto updatedPaymentDto);
    }
}
