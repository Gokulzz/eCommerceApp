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
    public class PaymentRepository: GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<string> CheckPaymentStatus(Guid orderId)
        {
            var payment_status= await dataContext.Payment.Where(x=>x.Equals(orderId)).FirstOrDefaultAsync();
            return payment_status.Status;
        }
    }
}
