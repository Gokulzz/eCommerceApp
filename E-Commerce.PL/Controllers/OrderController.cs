using eCommerceApp.BLL;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        [HttpGet("GetOrder")]
        public async Task<ApiResponse> GetOrder()
        {
            var order = await orderService.GetOrder();
            return order;
        }
        [HttpPost("AddOrder")]
        public async Task<ApiResponse> AddOrder()
        {
            var order= await orderService.AddOrder();
            return order;
        }
    }
}
