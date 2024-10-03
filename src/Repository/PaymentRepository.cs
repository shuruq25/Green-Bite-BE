using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;


namespace src.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateOneAsync(Payment newPayment);
        Task<bool> DeletePaymentAsync(Payment payment);
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(Guid id);
        Task<bool> UpdatePaymentAsync(Payment updatedPayment);
    }

    public class PaymentRepository : IPaymentRepository
    {
        protected readonly DbSet<Payment> _payment;
        protected readonly DatabaseContext _databaseContext;

        public PaymentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _payment = databaseContext.Set<Payment>();
        }

        public async Task<Payment> CreateOneAsync(Payment newPayment)
        {
            await _payment.AddAsync(newPayment);
            await _databaseContext.SaveChangesAsync();
            return newPayment;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _payment.Include(payment => payment.Order).ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _payment.Include(payment => payment.Order).FirstOrDefaultAsync(payment => payment.Id == id);
        }

        public async Task<bool> UpdatePaymentAsync(Payment updatedPayment)
        {
            _payment.Update(updatedPayment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePaymentAsync(Payment payment)
        {
            _payment.Remove(payment);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
