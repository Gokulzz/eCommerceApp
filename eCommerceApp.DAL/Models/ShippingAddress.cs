using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class ShippingAddress
    {
        public Guid addressId = Guid.NewGuid(); 
        public string FullName { get; set; }    
        public string Address { get; set; } 
        public string City { get; set; }
        public string State { get; set; }   
        public string PostalCode { get; set; }
        public string Country { get; set; } 
        public ICollection<User> Users { get; set; }
    }
}
