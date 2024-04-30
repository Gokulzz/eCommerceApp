using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public  class CartService : ICartService
    {
        private readonly IUserService userService;
        private readonly IUnitofWork unitofWork;
        public CartService(IUserService userService, IUnitofWork unitofWork)
        {
            this.userService = userService;
            this.unitofWork = unitofWork;
        }
        public async Task<ApiResponse> GetAll()
        {
            var userId = userService.GetCurrentId();
            var get_cartId = await unitofWork.CartRepository.GetCartId(userId);
            if(get_cartId==Guid.Empty)
            {
                throw new NotFoundException("Cart item could not be found");
            }
            var get_CartItem = await unitofWork.CartRepository.GetCartandCartItems(get_cartId);
            return new ApiResponse(200, "Cart item displayed successfully", get_CartItem);
        }

    }
}
