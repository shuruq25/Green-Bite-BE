using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace src.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateOneAsync(Payment newPayment);
        Task<bool> DeletePaymentAsync(Payment payment);
        Task<List<Payment>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Payment?> GetByIdAsync(Guid id);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbSet<Payment> _payment;
        private readonly DatabaseContext _databaseContext;

        public PaymentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _payment = databaseContext.Set<Payment>();
        }

        public async Task<Payment> CreateOneAsync(Payment newPayment)
        {
            try
            {
                await _payment.AddAsync(newPayment);
                await _databaseContext.SaveChangesAsync();
                return newPayment;
            }
            catch (DbUpdateException ex)
            {
                throw CustomException.InternalError("An error occurred while saving the payment.");
            }
        }

        public async Task<List<Payment>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _payment
                .Include(payment => payment.Order)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _payment.Include(payment => payment.Order)
                                 .FirstOrDefaultAsync(payment => payment.Id == id);
        }


        public async Task<bool> DeletePaymentAsync(Payment payment)
        {
            try
            {
                _payment.Remove(payment);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw  CustomException.InternalError("An error occurred while deleting the payment.");
            }
        }
    }
}
