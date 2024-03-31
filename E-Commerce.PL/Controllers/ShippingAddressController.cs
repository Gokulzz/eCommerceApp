using eCommerceApp.BLL;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class ShippingAddressController : Controller
    {
        public IShippingAddressService shippingAddressService { get; set; }
        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            this.shippingAddressService = shippingAddressService;
        }
        [HttpPost("AddShippingAddress")]
        public async Task<ApiResponse> AddShippingAddress([FromBody] ShippingAddressDTO addressDTO)
        {
            var add_address= await shippingAddressService.AddShippingAddress(addressDTO);
            return add_address;
        }
    }
}
