using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> UpdateOneAsync(Coupon updatedCoupon)
        {
            _coupon.Update(updatedCoupon);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}