using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public class OrderService : IOrderService
    {
        //I need to add the feature of cancelling the order.
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
        public OrderService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> GetAllOrders()
        {
            var all_Orders = await unitofWork.OrderRepository.GetAllAsync();
            return new ApiResponse(200, "All the ORDERS returned successfully", all_Orders);
        }
        public async Task<ApiResponse> GetOrder(Guid Id)
        {
            var order = await unitofWork.OrderRepository.GetAsync(Id);
            return new ApiResponse(200, $"Order of {Id} returned successfuly", order);

        }
        public async Task<ApiResponse> AddOrder(OrderDTO orderDTO)
        {

            var add_order = mapper.Map<Order>(orderDTO);
            await unitofWork.OrderRepository.PostAsync(add_order);
            await unitofWork.Save();
            return new ApiResponse(200, "New Product Added successfully", add_order);
        }
        public async Task<ApiResponse> UpdateOrder(OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> DeleteOrder(Guid orderId)
        {

            var delete_order = await unitofWork.ProductRepository.DeleteAsync(orderId);
            await unitofWork.Save();
            return new ApiResponse(200, "Category deleted successsfully", delete_order);


        }
    }
}
