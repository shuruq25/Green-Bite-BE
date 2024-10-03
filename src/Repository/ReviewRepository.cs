using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public class ReviewRepository
    {
        protected DbSet<Review> _review;
        protected DatabaseContext _databaseContext;

        public ReviewRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _review = databaseContext.Set<Review>();
        }

        public async Task<Review> CreateOneAsync(Review newReview)
        {
            await _review.AddAsync(newReview);
            await _databaseContext.SaveChangesAsync();
            return newReview;
        }

        public async Task<Review?> GetReviewAsync(Guid id)
        {
            return await _review.Include(i => i.Order).FirstOrDefaultAsync(i => i.ReviewId == id);
        }

        public async Task<List<Review>> GetReviewsAsync(PaginationOptions paginationOptions)
        {
            var result = _review.Include(r => r.Order).Where(r => r.Comment.ToLower().Contains(paginationOptions.Search));
            return await result
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewByOrderIdAsync(Guid orderId)
        {
            return await _review.Where(r => r.OrderId == orderId).Include(r => r.Order).ToListAsync();
        }

        public async Task<bool> DeleteOneAsync(Review review)
        {
            _review.Remove(review);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Review updatedReview)
        {
            _review.Update(updatedReview);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
