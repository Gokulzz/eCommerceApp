using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public class OrderService : IOrderService
    {
        //I need to add the feature of cancelling the order.
        private readonly IUnitofWork unitofWork;
        private readonly IUserService userService;
        public IMapper mapper;
        public OrderService(IUnitofWork unitofWork, IMapper mapper, IUserService userService)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<ApiResponse> GetAllOrders()
        {
            var userId= userService.GetCurrentId();
            var orderId = await unitofWork.OrderRepository.GetOrderId(userId);
            if (orderId == Guid.Empty)
            {
                throw new NotFoundException("Orders not found");
            }
            var get_orders = await unitofWork.OrderRepository.GetOrderandOrderDetails(orderId);
           
            return new ApiResponse(200, "Order displayed successfully", get_orders);

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
