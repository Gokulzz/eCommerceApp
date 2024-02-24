using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Stripe
{
    public class CustomerResource
    {
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }


        public CustomerResource(string id, string email, string name)
        {
            CustomerId= id;
            Email = email;
            Name = name;
        }

       
   
    }
}
