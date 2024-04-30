using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.DAL.Implementations
{
   public class CartRepository : GenericRepository<Cart> , ICartRepository
    {
        public CartRepository(DataContext dataContext) : base(dataContext)
        {
            
        }
        public async Task<Guid> GetCartId(Guid  userId)

        {
            var carts = await dataContext.Carts.ToListAsync();
            var getId = from items in carts
                        where items.userId == userId 
                        && items.cartCheckout=="No"
                        select items.cartID;
            return getId.FirstOrDefault(); 
        }
        public async Task<IEnumerable<Cart>> GetCartandCartItems(Guid cartId)
        {
            var get_carts = await dataContext.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).ToListAsync();
            var cartList= from item in get_carts
                          where item.cartID == cartId 
                          && item.cartCheckout=="No"
                          select item;  
            return cartList;

        }
    }
}
