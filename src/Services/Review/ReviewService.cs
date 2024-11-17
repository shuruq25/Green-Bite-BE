using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.ReviewDTO;

namespace src.Services.review
{
    public class ReviewService :IReviewService
    {
        protected readonly ReviewRepository _reviewRepo;
        protected readonly IMapper _mapper;

        public ReviewService(ReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        // Create a new review
        public async Task<ReviewReadDto> CreateOneAsync(ReviewCreateDto createDto, Guid userId)
        {
            if (createDto.Rating < 1 || createDto.Rating > 5)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(createDto.Rating),
                    "Rating must be between 1 and 5."
                );
            }

            var review = new Review
            {
                ProductId = createDto.ProductId,
                Comment = createDto.Comment,
                Rating = createDto.Rating,
                ReviewDate = DateTime.UtcNow,
                UserID = userId,
            };

            var reviewCreated = await _reviewRepo.CreateOneAsync(review);
            return _mapper.Map<ReviewReadDto>(reviewCreated);
        }

        // Get all reviews
        public async Task<List<ReviewReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            try
            {
                var reviewList = await _reviewRepo.GetReviewsAsync(paginationOptions);
                return _mapper.Map<List<Review>, List<ReviewReadDto>>(reviewList);
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(ex.Message);
            }
        }

        // Get a review by ID
        public async Task<ReviewReadDto> GetByIdAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(id);
            if (foundReview == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }
            return _mapper.Map<ReviewReadDto>(foundReview);
        }

        // Delete a review
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(id);
            if (foundReview == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            return await _reviewRepo.DeleteOneAsync(foundReview);
        }

        // Update a review
         // Update a review
        public async Task<bool> UpdateOneAsync(Guid productId, Guid reviewId, ReviewUpdateDto updateDto)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(reviewId);
            if (foundReview == null)
            {
                return false; // Review not found
            }

            // Map the updated values from updateDto to the existing review
            _mapper.Map(updateDto, foundReview);

            // Ensure that the product ID is passed to the repository
            return await _reviewRepo.UpdateOneAsync(productId, foundReview);
        }
    }
}
