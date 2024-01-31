using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Implementations;
using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        public IOrderDetailService orderDetail { get; set; }    
        public OrderDetailController(IOrderDetailService orderDetail)
        {
            this.orderDetail = orderDetail;
        }
        [HttpGet("GetOrderDetail")]
        public async Task<ApiResponse> GetOrderDetail(Guid id)
        {
            var order = await orderDetail.GetOrderDetails(id);
            return order;
        }
        [HttpGet("GetAllOrderDetail")]
        public async Task<ApiResponse> GetAllOrderDetail()
        {
            var allOrder = await orderDetail.GetAllOrderDetails();
            return allOrder;
        }
        [HttpPost("AddOrderDetail")]
        public async Task<ApiResponse> AddOrderDetail(OrderDetailDTO order)
        {
            var addOrder = await orderDetail.AddOrderDetails(order);
            return addOrder;

        }
        [HttpPut("UpdateOrderDetail")]
        public async Task<ApiResponse> UpdateOrderDetail(OrderDetailDTO order)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("DeleteOrderDetail")]
        public async Task<ApiResponse> DeleteOrderDetail(Guid id)
        {
            var delete_detail = await orderDetail.DeleteOrderDetails(id);
            return delete_detail;
        }

    }
}
