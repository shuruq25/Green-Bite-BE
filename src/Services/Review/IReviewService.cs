using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Utils;
using static src.DTO.ReviewDTO;

namespace src.Services.review
{
    public interface IReviewService
    {
        Task<ReviewReadDto> CreateOneAsync(ReviewCreateDto createDto);
        Task<List<ReviewReadDto>> GetAllAsync(PaginationOptions paginationOptions);
        Task<ReviewReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, ReviewUpdateDto updateDto);
    }
}
