using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;

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
            return await _review.FindAsync(id);
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _review.ToListAsync();
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