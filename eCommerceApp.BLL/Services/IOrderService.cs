using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IOrderService
    {
        public Task<ApiResponse> GetAllOrders();
        public Task<ApiResponse> GetOrder();
        public Task<ApiResponse> AddOrder();
        public Task<ApiResponse> UpdateOrder(OrderDTO order);
        public Task<ApiResponse> DeleteOrder(Guid orderId);
    }
}
