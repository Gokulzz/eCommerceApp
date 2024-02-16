using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class OrderDetailDTO
    {
        //Add the validator to check if the ordered quantity is less than the available quantity.
        public int Quantity { get; set; }
        public Guid productId { get; set; } 
        //public double subTotal { get; set; }
        
    }
}
