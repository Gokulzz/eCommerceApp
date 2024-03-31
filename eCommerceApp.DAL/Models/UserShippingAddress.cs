using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class UserShippingAddress
    {
        public Guid UsersuserId { get; set; }   
        public Guid shippingAddressesaddressId { get; set; }  
    }
}
