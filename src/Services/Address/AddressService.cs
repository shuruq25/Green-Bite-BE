using AutoMapper;
using src.Entity;
using src.Repository;
using static src.DTO.AddressDTO;

namespace src.Services
{
    public class AddressService : IAddressService
    {
        // repo : get the data frome database
        protected readonly AddressRepository _addressRepo;

        // mapper : convert the data type frome one to another
        protected readonly IMapper _mapper;

        // Constructer
        // DI the repository and mapper
        public AddressService(AddressRepository addressRepo, IMapper mapper)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        //create
        public async Task<AddressReadDto> CreatOneAsync(AddressCreateDto createDto)
        {
            var address = _mapper.Map<AddressCreateDto, Address>(createDto);

            var ddressCreate = await _addressRepo.CreateOneAsync(address);
            return _mapper.Map<Address, AddressReadDto>(ddressCreate);
        }

        // //get all
        public async Task<List<AddressReadDto>> GetAllAsync()
        {
            var addressList = await _addressRepo.GetAllAsync();
            return _mapper.Map<List<Address>, List<AddressReadDto>>(addressList);
        }

      

        //get by id
        public async Task<List<AddressReadDto>> GetByIdAsync(Guid id)
        {
            var foundAddress = await _addressRepo.GetAddressByIdAsync(id);
            // To Do :hande; error
            //throw
            
            return _mapper.Map<List<Address>, List<AddressReadDto>>(foundAddress);
        }
        

        // //delete
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundAddress = await _addressRepo.GetAddresstowByIdAsync(id);
            bool isDeleted = await _addressRepo.DeleteOneAsync(foundAddress);
            if (isDeleted)
            {
                return true;
            }
            return false;
        }

        //update
        public async Task<bool> UpdateOneAsync(Guid id, AddressUpdateDto updateDto)
        {
            var foundAddress = await _addressRepo.GetAddresstowByIdAsync(id);

            if (foundAddress == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundAddress);
            return await _addressRepo.UpdateOneAsync(foundAddress);
        }
            public async Task<AddressReadDto> GetByTowIdAsync(Guid id){
            var foundAddress = await _addressRepo.GetAddresstowByIdAsync(id);
            // To Do :hande; error
            //throw
            return _mapper.Map<Address, AddressReadDto>(foundAddress);  
            }

    }
}
