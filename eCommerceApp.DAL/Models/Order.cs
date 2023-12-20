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
        public Guid orderId = new Guid();
        public DateTime orderDate { get; set; }
        public double totalAmount { get; set; }
        public string Status { get; set; }
        public Guid userId { get; set; }
        public User user { get; set; }
        public ICollection<Orderdetails> orderDetails { get; set; }



    }
}
