using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        public Task<int> GetCartItemcount(Guid id);
        public Task<List<CartItem>> GetIDandQuantity(Guid cartId);


    }
}
