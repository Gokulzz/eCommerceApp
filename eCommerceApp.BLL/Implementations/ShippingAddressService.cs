using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public ShippingAddressService(IUnitofWork unitofWork, IMapper mapper, IUserService userService)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService = userService;
        }

        public Task<ApiResponse> GetShippingAddress(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> AddShippingAddress(ShippingAddressDTO addressDTO)
        {
           var map_address= mapper.Map<ShippingAddress>(addressDTO);
            var userAddress = new UserShippingAddress()
            {
                UsersuserId = userService.GetCurrentId(),
                shippingAddressesaddressId = map_address.addressId
            };
           await unitofWork.ShippingAddressRepository.PostAsync(map_address);
            await unitofWork.UserShippingAddressRepository.PostAsync(userAddress);
            await unitofWork.Save();
            return new ApiResponse(200, "Shipping Address added successfully", map_address);
        }

        public Task<ApiResponse> UpdateShippingAddress(Guid id, ShippingAddressDTO shippingAddressDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteShippingAddress(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
