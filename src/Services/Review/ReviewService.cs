using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.ReviewDTO;

namespace src.Services.review
{
    public class ReviewService : IReviewService
    {
        protected readonly ReviewRepository _reviewRepo;
        protected readonly IMapper _mapper;

        public ReviewService(ReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<ReviewReadDto> CreateOneAsync(ReviewCreateDto createDto)
        {
            var review = _mapper.Map<ReviewCreateDto, Review>(createDto);
            var reviewCreated = await _reviewRepo.CreateOneAsync(review);
            return _mapper.Map<Review, ReviewReadDto>(reviewCreated);
        }

        public async Task<List<ReviewReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var reviewList = await _reviewRepo.GetReviewsAsync(paginationOptions);
            return _mapper.Map<List<Review>, List<ReviewReadDto>>(reviewList);
        }

        public async Task<ReviewReadDto> GetByIdAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(id);
            return _mapper.Map<Review, ReviewReadDto>(foundReview);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(id);
            if (foundReview != null)
            {
                bool isDeleted = await _reviewRepo.DeleteOneAsync(foundReview);

                if (isDeleted)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, ReviewUpdateDto updateDto)
        {
            var foundReview = await _reviewRepo.GetReviewAsync(id);

            if (foundReview == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundReview);
            return await _reviewRepo.UpdateOneAsync(foundReview);
        }
    }
}
