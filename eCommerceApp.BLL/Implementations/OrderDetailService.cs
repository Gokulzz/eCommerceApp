using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Crypto;

namespace eCommerceApp.BLL.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
        private readonly IProductService productService;
        public IUserService userService { get; set; }
        public OrderDetailService(IUnitofWork unitofWork, IMapper mapper, IProductService productService, IUserService userService)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.productService = productService;
            this.userService= userService;
        }
        public async Task<ApiResponse> GetAllOrderDetails()
        {
            var orderdetails = await unitofWork.OrderdetailRepository.GetOrderdetailswithProduct();
            if(orderdetails == null)
            {
                throw new NotFoundException("Order details not found");
            }
            return new ApiResponse(200, "All the ORDERS returned successfully", orderdetails);
        }
        public async Task<ApiResponse> GetOrderDetails(Guid Id)
        {
            var order = await unitofWork.OrderdetailRepository.GetAsync(Id);
            return new ApiResponse(200, $"Order of {Id} returned successfuly", order);

        }
        public async Task<ApiResponse> GetOrderDetailCount()
        {
            var userId = userService.GetCurrentId();
            var orderId = await unitofWork.OrderRepository.GetOrderId(userId);
            if(orderId== Guid.Empty)
            {
                throw new NotFoundException("Orders not found");
            }
            var get_Count = await unitofWork.OrderdetailRepository.GetOrderdetailcount(orderId);
            return new ApiResponse(200, $"Order count returned successfully", get_Count);
            
            
        }
        public async Task<ApiResponse> AddOrderDetails(OrderDetailDTO orderDetailsDTO )
        {

            var add_order = mapper.Map<Orderdetails>(orderDetailsDTO);
            add_order.orderId = Guid.NewGuid();
            var find_price = await productService.GetProductprice(orderDetailsDTO.productId);
            add_order.subTotal= calculateTotal(orderDetailsDTO.Quantity, find_price);   

            await unitofWork.OrderdetailRepository.PostAsync(add_order);
            var order = new Order()
            {
                orderId = add_order.orderId,
                orderDate = DateTime.Now,
                userId = userService.GetCurrentId(),
                Status = "hold",
                totalAmount = add_order.subTotal,
                grandTotal = add_order.subTotal + 0.13 * add_order.subTotal


            };
            await unitofWork.OrderRepository.PostAsync(order);
            await unitofWork.Save();
            return new ApiResponse(200, "New Product Added successfully", add_order);
        }
        
        public async Task<ApiResponse> UpdateOrderDetails(OrderDetailDTO orderDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> DeleteOrderDetails(Guid orderId)
        {

            var delete_order = await unitofWork.OrderdetailRepository.DeleteAsync(orderId);
            await unitofWork.Save();
            return new ApiResponse(200, "Category deleted successsfully", delete_order);



        }
        public double calculateTotal(int quantity, double price)
        {
            return quantity * price;
        }
       
     }
}

