using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.DAL.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        
        
        public CartItemRepository(DataContext dataContext) : base(dataContext)
        {
          
            

        }
        public async Task<int> GetCartItemcount(Guid id)
        {
            var get_cartItem = await dataContext.CartItems.Where(x=>x.CartID== id).ToListAsync();  
            return get_cartItem.Count;
        }
       public async Task<List<CartItem>> GetIDandQuantity(Guid cartId)
       {
           var cartItems = await dataContext.CartItems.ToListAsync();
           var get_cartItem = from x in cartItems
                       where x.CartID == cartId
                       select new CartItem()
                       {
                           ProductID = x.ProductID,
                           Quantity = x.Quantity,
                       };

            return get_cartItem.ToList();
       }


    }
}
