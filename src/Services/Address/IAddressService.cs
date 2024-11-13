using static src.DTO.AddressDTO;

namespace src.Services
{
    public interface IAddressService
    {
        //create
        Task<AddressReadDto> CreatOneAsync(AddressCreateDto createDto);

        //get all
        Task<List<AddressReadDto>> GetAllAsync();

        //get all
        // add Pagination
        // Task<List<AddressReadDto>> GetAllAsync(PaginationOptions paginationOptions);

        //get by id
        Task<List<AddressReadDto>> GetByIdAsync(Guid id);
    Task<AddressReadDto> GetByTowIdAsync(Guid id);


        //delete
        Task<bool> DeleteOneAsync(Guid id);

        //update
        Task<bool> UpdateOneAsync(Guid id, AddressUpdateDto updateDto);
    }
}
