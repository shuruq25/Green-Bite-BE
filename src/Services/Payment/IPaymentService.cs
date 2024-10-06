using src.DTO;
namespace src.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO.PaymentReadDto>> GetAllPayments(int page = 1, int pageSize = 10);
         Task<PaymentDTO.PaymentReadDto?> GetPaymentById(Guid id);
        Task<bool> DeletePaymentById(Guid id);
        Task<PaymentDTO.PaymentReadDto> CreatePayment(PaymentDTO.PaymentCreateDto newPaymentDto);




    }
}
