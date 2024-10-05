using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

namespace src.Repository
{
    public class CouponRepository
    {
        protected DbSet<Coupon> _coupon;

        protected DatabaseContext _databaseContext;

        public CouponRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            _coupon = databaseContext.Set<Coupon>();
        }

        // methods


        // create a new Coupon:

        public async Task<Coupon> CreateOneAsync(Coupon newCoupon)
        {
            await _coupon.AddAsync(newCoupon);
            await _databaseContext.SaveChangesAsync();

            return newCoupon;
        }

        // get all Coupons:

        public async Task<List<Coupon>> GetAllAsync()
        {
            return await _coupon.ToListAsync();
        }

        // get all Coupons:
        // add the Pagination

        // public async Task<List<Coupon>> GetAllAsync(PaginationOptions paginationOptions)
        // {
        //     var result = _coupon.Where(c => c.Code.ToLower().Contains(paginationOptions.Search));
        //     return await result
        //         .Skip(paginationOptions.Offset)
        //         .Take(paginationOptions.Limit)
        //         .ToListAsync();
        // }

        // get by Coupon by ID

        public async Task<Coupon?> GetCouponByIdAsync(Guid id)
        {
            return await _coupon.FindAsync(id);
        }

        //delete Coupon:

        public async Task<bool> DeleteOneAsync(Coupon Coupon)
        {
            _coupon.Remove(Coupon);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //Update Coupon:

      
    }
}
