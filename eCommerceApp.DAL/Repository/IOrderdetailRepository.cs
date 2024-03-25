using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IOrderdetailRepository : IGenericRepository<Orderdetails>
    {
        public Task<List<Orderdetails>> GetOrderdetailswithProduct();
        public Task<int> GetOrderdetailcount(Guid orderId);
    }
}
