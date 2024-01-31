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
        //when user want to buy product straight away without adding it in the cart then we want to know the
        //details of the product user wants to buy  
        public Guid productId { get; set; }
        public Product product { get; set; }    
    }
}
 
