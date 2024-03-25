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
   public class OrderDetailRepository : GenericRepository<Orderdetails>,  IOrderdetailRepository
    {
        public OrderDetailRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<List<Orderdetails>> GetOrderdetailswithProduct()
        {
            var orderDetails = await dataContext.OrderDetails.Include(x => x.product).ToListAsync();
            return orderDetails;
        }
        public async Task<int> GetOrderdetailcount(Guid id)
        {
            var get_order= await dataContext.OrderDetails.Where(x=>x.orderId== id).ToListAsync();
            return get_order.Count;
        }
    }
}
