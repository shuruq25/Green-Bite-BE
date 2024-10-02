using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Utils;
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
        Task<AddressReadDto> GetByIdAsync(Guid id);

        //delete
        Task<bool> DeleteOneAsync(Guid id);

        //update
        Task<bool> UpdateOneAsync(Guid id, AddressUpdateDto updateDto);
    }
}
