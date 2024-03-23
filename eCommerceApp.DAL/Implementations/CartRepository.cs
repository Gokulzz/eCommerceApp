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
                        select items.cartID;
            return getId.FirstOrDefault(); 
        }
    }
}
