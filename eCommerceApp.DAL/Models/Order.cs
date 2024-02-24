using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Order
    {
        [Key]
        public Guid orderId { get; set; }
        public DateTime orderDate { get; set; }
        //this is the total amount before tax
        public double totalAmount { get; set; }
        //total amount after tax 
        public double grandTotal { get; set; }  
        public string Status { get; set; }
        public Guid userId { get; set; }
        public User user { get; set; }
        public Payment payment { get; set; }
        public ICollection<Orderdetails> orderDetails { get; set; }



    }
}
