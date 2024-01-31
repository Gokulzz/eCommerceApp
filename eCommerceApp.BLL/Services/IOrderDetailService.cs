using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IOrderDetailService
    {
        public Task<ApiResponse> GetAllOrderDetails();
        public Task<ApiResponse> GetOrderDetails(Guid orderId);
        public Task<ApiResponse> AddOrderDetails(OrderDetailDTO DTO);
        public Task<ApiResponse> UpdateOrderDetails(OrderDetailDTO DTO);
        public Task<ApiResponse> DeleteOrderDetails(Guid orderId);
    }
}
