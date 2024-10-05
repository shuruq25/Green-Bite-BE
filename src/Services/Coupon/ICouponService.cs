using static src.DTO.CouponDTO;

namespace src.Services
{
    public interface ICouponService
    {
        //create
        Task<CouponReadDto> CreateOneAsync(CouponCreateDto createDto);
        //get all
        Task<List<CouponReadDto>> GetAllAsync();

        //get all
        // add Pagination
        // Task<List<CouponReadDto>> GetAllAsync(PaginationOptions paginationOptions);

        //get by id
        Task<CouponReadDto> GetByIdAsync(Guid id);

        //delete
        Task<bool> DeleteOneAsync(Guid id);

        //update
    }
}
