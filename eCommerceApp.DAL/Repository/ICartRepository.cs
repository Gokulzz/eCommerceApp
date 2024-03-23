using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<Guid> GetCartId(Guid userId);
    }
}
