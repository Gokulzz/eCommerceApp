using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
       
       public IOrderService orderService { get; set; }
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;   
        }
        [HttpGet("GetAllOrder")]
        public async Task<ApiResponse> GetAllOrder()
        {
            var orders= await orderService.GetAllOrders();
            return orders;
        }
    }
}
