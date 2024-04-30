using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface ICartItemService
    {
        public Task<ApiResponse> GetCartItem(Guid id);
        public Task<ApiResponse> GetAllCartItem();
        public  Task<ApiResponse> AddCartItem(CartItemDTO cartItem);
        public Task<ApiResponse> GetCartItemCount();
        public Task<ApiResponse> UpdateCartItem(CartItemDTO cartItem, Guid id);
        public Task<ApiResponse> DeleteCartItem(Guid id);
        
    }
}
