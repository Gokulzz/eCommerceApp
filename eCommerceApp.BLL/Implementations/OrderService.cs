using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.Identity.Client;
using Stripe.Climate;
using Order = eCommerceApp.DAL.Models.Order;

namespace eCommerceApp.BLL.Implementations
{
    public class OrderService : IOrderService
    {
        //I need to add the feature of cancelling the order.
        private readonly IUnitofWork unitofWork;
        private readonly IMessageProducer producer;
        private readonly IUserService userService;
        private readonly IProductService productService;
        public IMapper mapper;
        public OrderService(IUnitofWork unitofWork, IMapper mapper, IUserService userService, IProductService productService, IMessageProducer producer )
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService = userService;
            this.productService = productService;  
            this.producer = producer;
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
        
        public async Task<ApiResponse> GetOrder()
        {
            var userId = userService.GetCurrentId();
            var orderId = await unitofWork.OrderRepository.GetOrderId(userId);
            if (orderId == Guid.Empty)
            {
                throw new NotFoundException("Orders not found");
            }
            var get_orders= await unitofWork.OrderRepository.GetAsync(orderId); 
            var map_order= mapper.Map<CheckOutOrderDTO>(get_orders);
            return new ApiResponse(200, "order returned successfully", map_order);

        }
        public async Task<ApiResponse> AddOrder()
        {

            //var add_order = mapper.Map<Order>(orderDTO);
            //await unitofWork.OrderRepository.PostAsync(add_order);
            //await unitofWork.Save();
            //return new ApiResponse(200, "New Product Added successfully", add_order);
            var userId = userService.GetCurrentId();
            var orderId = await unitofWork.OrderRepository.GetOrderId(userId);

            var cartId= await unitofWork.CartRepository.GetCartId(userId);
           
          
                var cartItem = await unitofWork.CartItemRepository.GetIDandQuantity(cartId);
               
                if (orderId == Guid.Empty)
                {
                    orderId = Guid.NewGuid(); // Generate new orderId
                    var order = await add_Order(userId, orderId, cartItem);
                    await unitofWork.OrderRepository.PostAsync(order);
                    producer.SendMessage(order);
                }
                else
                {
                    var order = await UpdateCurrentOrder(orderId, cartItem);
                    await unitofWork.OrderRepository.UpdateAsync(orderId, order);
                }
                foreach (var item in cartItem)
                {
                    var find_price = await productService.GetProductprice(item.ProductID);
                    var orderItem = new Orderdetails
                    {
                        productId = item.ProductID,
                        orderId = orderId,
                        Quantity = item.Quantity,
                        subTotal = calculateTotal(item.Quantity, find_price)
                    };
                    
                    await unitofWork.OrderdetailRepository.PostAsync(orderItem);
              
                }
                await unitofWork.Save();
               

            return new ApiResponse(200, "Order placed successfully", cartItem);
            
            
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
        public async Task<Order> add_Order(Guid userId, Guid neworderId, List<CartItem> cartItem)
        {
            double totalAmount = 0;
            foreach (var item in cartItem)
            {
                var find_price = await productService.GetProductprice(item.ProductID);
                totalAmount += calculateTotal(item.Quantity, find_price);
            }

            double grandTotal = totalAmount * 1.13;
            var order = new Order
            {
                orderDate = DateTime.Now,
                orderId = neworderId,
                userId = userId,
                Status = "Hold",

                totalAmount = totalAmount,
                grandTotal = grandTotal
            };
            return order;


        }
        public async Task<Order> UpdateCurrentOrder(Guid orderId, List<CartItem> cartItem)
        {
            var order = await unitofWork.OrderRepository.GetAsync(orderId);
            if (order != null)
            {
                order.totalAmount = 0;
                foreach (var item in cartItem)
                {
                    var find_price = await productService.GetProductprice(item.ProductID);
                    order.totalAmount += calculateTotal(item.Quantity, find_price);
                }
                order.grandTotal = order.totalAmount * 1.13;


            }
            return order;
        }
        public double calculateTotal(int quantity, double price)
        {
            return quantity * price;
        }
    }
}
