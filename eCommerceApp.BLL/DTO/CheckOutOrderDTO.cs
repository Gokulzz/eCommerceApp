using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class CheckOutOrderDTO
    {
        public Guid orderId { get; set; }   
        public double TotalPrice { get; set; }  
        public double GrandTotal { get; set; }  
    }
}
