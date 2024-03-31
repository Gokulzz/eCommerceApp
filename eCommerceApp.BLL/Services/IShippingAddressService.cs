using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IShippingAddressService
    {
        public Task<ApiResponse> GetShippingAddress(Guid id);
        public Task<ApiResponse> AddShippingAddress(ShippingAddressDTO addressDTO);
        public Task<ApiResponse> UpdateShippingAddress(Guid id, ShippingAddressDTO shippingAddressDTO);
        public Task<ApiResponse> DeleteShippingAddress(Guid id);
    }
}
