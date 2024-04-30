using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IPaymentMethodRepository : IGenericRepository<CustomerPaymentMethod>
    {
        public Task<Guid> GetPaymentMethodId(Guid id);
        public  Task<IEnumerable<CustomerPaymentMethod>> GetPaymentMethod(Guid id);
    }
}
