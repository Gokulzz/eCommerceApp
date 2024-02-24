using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class PaymentDTO
    {
        public Guid paymentMethodId { get; set; }   
        public Guid orderId { get; set; }
        public double amount { get; set; }  
    }
}
