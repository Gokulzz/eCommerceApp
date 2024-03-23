using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Implementations;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.BLL.Implementations
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        public IUserService userService { get; set; }
        public CartItemService(IUnitofWork unitofWork, IMapper mapper, IUserService userService, IProductService productService)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService = userService;
            this.productService = productService;
        }

        public async Task<ApiResponse> GetCartItem(Guid id)
        {
            var get_item = await unitofWork.CartItemRepository.GetAsync(id);
            return new ApiResponse(200, $"Item of {id} is returned", get_item);
        }

        public async Task<ApiResponse> GetAllCartItem()
        {
            var get_allItem = await unitofWork.CartItemRepository.GetAllAsync();
            return new ApiResponse(200, "All the item returned successfully", get_allItem);
        }

        public async Task<ApiResponse> AddCartItem(CartItemDTO cartItem)
        {
            var userId = userService.GetCurrentId();
            try
            {
                // Retrieve or generate cartId
                var cartId = await unitofWork.CartRepository.GetCartId(userId);
                if (cartId == Guid.Empty)
                {
                    cartId = Guid.NewGuid(); // Generate new cartId
                    var cart = await AddCart(userId, cartId);
                    await unitofWork.CartRepository.PostAsync(cart);
                }

                // Retrieve or generate orderId
                var orderId = await unitofWork.OrderRepository.GetOrderId(userId);
                if (orderId == Guid.Empty)
                {
                    orderId = Guid.NewGuid(); // Generate new orderId
                    var order = await AddOrder(userId, orderId, cartItem);
                    await unitofWork.OrderRepository.PostAsync(order);
                }
                else
                {
                    var order= await UpdateCurrentOrder(orderId, cartItem);
                    await unitofWork.OrderRepository.UpdateAsync(orderId, order);
                }

                // Add cart items
                foreach (var item in cartItem.items)
                {
                    var find_price = await productService.GetProductprice(item.productId);

                    var cartItemEntity = new CartItem
                    {
                        ProductID = item.productId,
                        CartID = cartId,
                        Quantity = item.quantity
                    };

                    var orderItem = new Orderdetails
                    {
                        productId = item.productId,
                        orderId = orderId,
                        Quantity = item.quantity,
                        subTotal = calculateTotal(item.quantity, find_price)
                    };
                    await unitofWork.CartItemRepository.PostAsync(cartItemEntity);
                    await unitofWork.OrderdetailRepository.PostAsync(orderItem);
                }

                await unitofWork.Save();

                return new ApiResponse(200, "Cart items added successfully", cartItem);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Exception during AddCartItem: {ex.Message}");
                return new ApiResponse(500, "Error adding cart items", "");
            }
        }


        public async Task<ApiResponse> UpdateCartItem(CartItemDTO item, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> DeleteCartItem(Guid id)
        {
            var get_Item = await unitofWork.CartItemRepository.GetAsync(id);
            return new ApiResponse(200, $"Item of {id} deleted successfully", get_Item);
        }

        public double calculateTotal(int quantity, double price)
        {
            return quantity * price;
        }

        public async Task<Cart> AddCart(Guid userId, Guid newcartId)
        {
            var cart = new Cart
            {
                userId = userId,
                cartID = newcartId
            };
            return cart;
          
        }

        public async Task<Order> AddOrder(Guid userId, Guid neworderId, CartItemDTO cartItem)
        {
            double totalAmount = 0;
            foreach (var item in cartItem.items)
            {
                var find_price = await productService.GetProductprice(item.productId);
                totalAmount += calculateTotal(item.quantity, find_price);
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
        public async Task<Order> UpdateCurrentOrder(Guid orderId, CartItemDTO cartItem)
        {
            var order = await unitofWork.OrderRepository.GetAsync(orderId);
            if (order != null)
            {
                foreach(var item in cartItem.items)
                {
                    var find_price = await productService.GetProductprice(item.productId);
                    order.totalAmount+= calculateTotal(item.quantity, find_price);
                    order.grandTotal = order.totalAmount * 1.13;
                }


            }
            return order;
        }
    }
    
}
