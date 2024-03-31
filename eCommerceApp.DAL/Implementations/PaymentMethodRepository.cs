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
    public class PaymentMethodRepository : GenericRepository<CustomerPaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<Guid> GetPaymentMethodId(Guid id)
        {
            var paymentMethod = await dataContext.PaymentMethod.ToListAsync();
            var get_Id = from p in paymentMethod
                         where p.userId == id
                         select p.paymentMethodId;
            return get_Id.FirstOrDefault();

        }
    }
}
