using Microsoft.EntityFrameworkCore;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public class ReviewRepository
    {
        protected DbSet<Review> _review;
        protected DbSet<Product> _products;
        protected DatabaseContext _databaseContext;

        public ReviewRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _review = databaseContext.Set<Review>();
            _products = databaseContext.Set<Product>();
        }

        // Create a new review
           public async Task<Review> CreateOneAsync(Review newReview)
        {
            var productExists = await _products.AnyAsync(p => p.Id == newReview.ProductId);
            if (!productExists)
            {
                throw new KeyNotFoundException($"Product with ID {newReview.ProductId} not found.");
            }

            try
            {
                await _review.AddAsync(newReview);
                await _databaseContext.SaveChangesAsync();
                return newReview;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Failed to create review: " + ex.Message);
            }
        }

        

        // Get a review by ID
        public async Task<Review?> GetReviewAsync(Guid id)
        {
            try
            {
                return await _review
                    .FindAsync(id);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error fetching review: " + ex.Message);
            }
        }

        // Get all reviews with pagination
        public async Task<List<Review>> GetReviewsAsync(PaginationOptions paginationOptions)
        {
            try
            {
                var result = _review.Where(r => r.Comment.ToLower().Contains(paginationOptions.Name));
                return await result
                    .Skip(paginationOptions.Offset)
                    .Take(paginationOptions.Limit)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error fetching reviews: " + ex.Message);
            }
        }

        // Delete a review
        public async Task<bool> DeleteOneAsync(Review review)
        {
            try
            {
                _review.Remove(review);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError("Error deleting review: " + ex.Message);
            }
        }

        // Update a review
// Update a review in the ReviewRepository
public async Task<bool> UpdateOneAsync(Guid productId, Review updatedReview)
{
    var product = await _products.FirstOrDefaultAsync(p => p.Id == productId);

    if (product == null)
    {
        throw new KeyNotFoundException($"Product with ID {productId} not found.");
    }

    var existingReview = await _review.FirstOrDefaultAsync(r => r.ReviewId == updatedReview.ReviewId);

    if (existingReview == null)
    {
        throw new KeyNotFoundException($"Review with ID {updatedReview.ReviewId} not found.");
    }

    // Update review fields
    existingReview.Rating = updatedReview.Rating;
    existingReview.Comment = updatedReview.Comment;
    existingReview.ReviewDate = updatedReview.ReviewDate;

    // Calculate the average rating for the product
    var productReviews = await _review.Where(r => r.ProductId == productId).ToListAsync();
    product.AveReviews = productReviews.Any() ? productReviews.Average(r => r.Rating) : 0;

    await _databaseContext.SaveChangesAsync();

    return true;
}


    }
}
