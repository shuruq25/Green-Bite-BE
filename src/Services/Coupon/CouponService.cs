using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.Repository;
using static src.DTO.CouponDTO;
using src.Entity;

namespace src.Services
{
    public class CouponService : ICouponService
    {    protected readonly CouponRepository _couponRepo;
        protected readonly IMapper _mapper;

        public CouponService(CouponRepository couponRepository , IMapper mapper){
            _couponRepo = couponRepository;
            _mapper = mapper;
        }

        
        //create
        public async Task<CouponReadDto> CreatOneAsync(CouponCreateDto createDto)
        {
            var Coupon = _mapper.Map<CouponCreateDto, Coupon>(createDto);

            var ddressCreate = await _couponRepo.CreateOneAsync(Coupon);
            return _mapper.Map<Coupon, CouponReadDto>(ddressCreate);
        }

        // //get all
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

        // //update
        public async Task<bool> UpdateOneAsync(Guid id, CouponUpdateDto updateDto)
        {
            var foundCoupon = await _couponRepo.GetCouponByIdAsync(id);

            if (foundCoupon == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundCoupon);
            return await _couponRepo.UpdateOneAsync(foundCoupon);
        
    }
}}