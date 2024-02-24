using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Stripe
{
    public class CreateCustomerResource
    {
        public string Email { get; set; }   
        public string Name { get; set; }    
        public CreateCardResource card { get; set; }

    }
}
