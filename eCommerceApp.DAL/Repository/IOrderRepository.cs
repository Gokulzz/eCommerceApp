using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public  Task<double> GetOrderAmount(Guid id);
        public Task<Guid> GetOrderId(Guid id);
        
    }
    
    
}
