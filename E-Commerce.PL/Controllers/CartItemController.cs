using System.Reflection.Metadata.Ecma335;
using eCommerceApp.BLL;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        public ICartItemService cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }
        [HttpGet("GetCartItem")]
        public async Task<ApiResponse> GetCartItem(Guid id)
        {
            var get_Item = await cartItemService.GetCartItem(id);
            return get_Item;
        }
        [HttpGet("GetAllCartItem")]
        public async Task<ApiResponse> GetAllCartItem()
        {
            var get_allItem= await cartItemService.GetAllCartItem();
            return get_allItem;
        }
        [HttpGet("GetCartItemCount")]
        public async Task<ApiResponse> GetCartItemCount()
        {
            var getCount = await cartItemService.GetCartItemCount();
            return getCount;
        }
        [HttpPost("AddCartItem")]
        public async Task<ApiResponse> AddCartItem([FromBody] CartItemDTO itemDTO)
        {

            var add_Item= await cartItemService.AddCartItem(itemDTO);
            return add_Item;
        }
        [HttpPut("UpdateCartItem")]
        public async Task<ApiResponse> UpdateCartItem(CartItemDTO itemDTO, Guid id)
        {
            var update_item = await cartItemService.UpdateCartItem(itemDTO, id);
            return update_item;
        }
        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<ApiResponse> DeleteCartItem(Guid id)
        {
            var delete_Item = await cartItemService.DeleteCartItem(id);
            return delete_Item;
        }

    }
}
