using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Payment
    {

        [Key]
        public Guid paymentId = Guid.NewGuid();
        public double Amount { get; set; }
        public DateTime paymentDate { get; set; }
        public string Status { get; set; }
        public Guid paymentMethodId { get; set; }
        // A single payment is associated with single payment method.
        public Order order { get; set; }
        public Guid orderId { get; set; }    
        public CustomerPaymentMethod paymentmethod { get; set; }
        
        

    }
}
