using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class OrderDTO
    {
        public DateTime orderDate { get; set; }
        public double totalAmount { get; set; }
        public string Status { get; set; }
    }
}
