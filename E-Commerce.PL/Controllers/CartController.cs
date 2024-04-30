using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
      public ICartService cartService { get; set; } 
      public CartController(ICartService cartService)
      {
            this.cartService = cartService;
      }
        [HttpGet("GetCart")]
        public async Task<ApiResponse> GetCart()
        {
            var get_cartItems = await cartService.GetAll();
            return get_cartItems;
        }
    }
}
