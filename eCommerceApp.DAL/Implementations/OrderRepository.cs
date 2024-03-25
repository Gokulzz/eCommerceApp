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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<double> GetOrderAmount(Guid id)
        {
            var amount= await dataContext.Orders.FindAsync(id);
            return amount.totalAmount;
        }
        public async Task<Guid> GetOrderId(Guid userId)
        {
            var get_orders = await dataContext.Orders.ToListAsync();
            var getId = from orders in get_orders
                        where orders.userId == userId
                        select orders.orderId;
            return getId.FirstOrDefault();
        }
        public async Task<IEnumerable<Order>> GetOrderandOrderDetails(Guid orderId)
        {
            var get_orders = await dataContext.Orders.Include(x => x.orderDetails).ThenInclude(x=>x.product).ToListAsync();

            var orderList = from orders in get_orders
                            where orders.orderId == orderId
                            && orders.Status == "Hold"
                            select orders;
            return orderList;



        }
        
    }
}
