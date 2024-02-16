using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
            try
            {
                if (cartItem.items == null || !cartItem.items.Any())
                {
                    return new ApiResponse(400, "No items provided for the cart", "");
                }

                var cartId = Guid.NewGuid();
                var OrderId = Guid.NewGuid();
                
              

                var order = new Order
                {
                    orderDate = DateTime.Now,
                    orderId = OrderId,
                    userId = userService.GetCurrentId(),
                    Status = "Hold",
                };

                order.totalAmount = 0;
                order.grandTotal = 0;

                foreach (var item in cartItem.items)
                {
                    var cartItemEntity = new CartItem
                    {
                        ProductID = item.productId,
                        CartID = cartId,
                        Quantity = item.quantity
                    };
                    var find_price = await productService.GetProductprice(item.productId);

                    await unitofWork.CartItemRepository.PostAsync(cartItemEntity);

                    var orderItem = new Orderdetails
                    {
                        productId = item.productId,
                        orderId = OrderId,
                        Quantity = item.quantity,
                        subTotal = calculateTotal(item.quantity, find_price)
                    };

                    await unitofWork.OrderdetailRepository.PostAsync(orderItem);

                    // Update order totals for each item
                    order.totalAmount += calculateTotal(item.quantity, find_price);
                    order.grandTotal += calculateTotal(item.quantity, find_price) + 0.13 * calculateTotal(item.quantity, find_price);
                }

                // Save the order with updated totals
                await unitofWork.OrderRepository.PostAsync(order);

                var cart = new Cart
                {
                    userId = userService.GetCurrentId(),
                    cartID = cartId,
                };

                await unitofWork.CartRepository.PostAsync(cart);
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

    
    public double calculateTotal(int quantity, double price)
        {
            return quantity * price;
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
    }
}
   
