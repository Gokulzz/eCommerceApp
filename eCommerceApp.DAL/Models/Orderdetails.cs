using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Orderdetails
    {
        [Key]
        public Guid orderDetailId { get; set; }
        public Guid orderId { get; set; }
        public Order order { get; set; }
        public int Quantity { get; set; }
        public double subTotal { get; set; }
    }
}
