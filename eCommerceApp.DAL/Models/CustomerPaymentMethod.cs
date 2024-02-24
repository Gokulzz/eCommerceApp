using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class CustomerPaymentMethod
    {
        [Key]
        public Guid paymentMethodId = Guid.NewGuid();
        public User user { get; set; }
        public Guid userId { get; set; }    
        public string stripeCustomerId { get; set; }
        public string Last4 { get; set; }
        public string Brand { get; set; }
        public string  expMonth { get; set; }
        public string expYear { get; set; }
        public string Type { get; set; }
        //User can make multiple payment using same payment method
        public ICollection<Payment>? payments { get; set; }

    }
}
