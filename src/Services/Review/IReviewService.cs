using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static src.DTO.ReviewDTO;

namespace src.Services.Review
{
    public interface IReviewService
    {
        Task<ReviewReadDto> CreateOneAsync(ReviewCreateDto createDto);
        Task<List<ReviewReadDto>> GetAllAsync();
        Task<ReviewReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ReviewUpdateDto updateDto);
    }
}
