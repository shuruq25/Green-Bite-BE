using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.UserDTO;

namespace src.Services.UserService
{
    public class UserService : IUserService
    {
        protected readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;

        public UserService(UserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(createDto);
            var userCreated = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(userCreated);
        }

        public async Task<List<UserReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var userList = await _userRepo.GetAllAsync(paginationOptions);
            return _mapper.Map<List<User>, List<UserReadDto>>(userList);
        }

        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            return _mapper.Map<User, UserReadDto>(foundUser);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser != null)
            {
                bool isDeleted = await _userRepo.DeleteOneAsync(foundUser);

                if (isDeleted)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);

            if (foundUser == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundUser);
            return await _userRepo.UpdateOneAsync(foundUser);
        }
    }
}
