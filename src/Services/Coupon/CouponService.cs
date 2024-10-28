using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.CouponDTO;

namespace src.Services
{
    public class CouponService : ICouponService
    {
        protected readonly CouponRepository _couponRepo;
        protected readonly IMapper _mapper;

        public CouponService(CouponRepository couponRepository, IMapper mapper)
        {
            _couponRepo = couponRepository;
            _mapper = mapper;
        }

        //create
        public async Task<CouponReadDto> CreateOneAsync(CouponCreateDto createDto)
        {
            if (createDto.DiscountPercentage < 0 || createDto.DiscountPercentage > 100)
            {
                throw new ArgumentException("Discount percentage must be between 0 and 100.");
            }

            var expireUtc = DateTime.SpecifyKind(createDto.Expire, DateTimeKind.Utc);

            var newCoupon = new Coupon
            {
                CouponId = Guid.NewGuid(),
                Code = createDto.Code,
                DiscountPercentage = createDto.DiscountPercentage,
                Expire = expireUtc,
                CreatedDate = DateTime.UtcNow
            };

            await _couponRepo.CreateOneAsync(newCoupon);

            return new CouponReadDto
            {
                CouponId = newCoupon.CouponId,
                Code = newCoupon.Code,
                DiscountPercentage = newCoupon.DiscountPercentage,
                Expire = newCoupon.Expire
            };
        }


        //get all
        public async Task<List<CouponReadDto>> GetAllAsync()
        {
            var CouponList = await _couponRepo.GetAllAsync();
            return _mapper.Map<List<Coupon>, List<CouponReadDto>>(CouponList);
        }

      

        // //get by id
        public async Task<CouponReadDto> GetByIdAsync(Guid id)
        {
            var foundCoupon = await _couponRepo.GetCouponByIdAsync(id);
            // To Do :hande; error
            //throw
            return _mapper.Map<Coupon, CouponReadDto>(foundCoupon);
        }

        // //delete
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCoupon = await _couponRepo.GetCouponByIdAsync(id);
            bool isDeleted = await _couponRepo.DeleteOneAsync(foundCoupon);
            if (isDeleted)
            {
                return true;
            }
            return false;
        }


    }
}
