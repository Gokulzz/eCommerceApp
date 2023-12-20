using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Paymentmethod
    {
        public Guid paymentMethodId = new Guid();
        public User user { get; set; }
        public Guid userId { get; set; }    
        public Guid? stripeCustomerId { get; set; }
        public int Last4 { get; set; }
        public string Brand { get; set; }
        public int expMonth { get; set; }
        public int expYear { get; set; }
        public string Type { get; set; }
        //User can make multiple payment using same payment method
        public ICollection<Payment> payments { get; set; }

    }
}
