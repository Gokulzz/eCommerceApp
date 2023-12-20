using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.DAL.Implementations
{
    public class PaymentMethodRepository : GenericRepository<Paymentmethod>, IPayMethodPaymentRepository
    {
        public PaymentMethodRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
