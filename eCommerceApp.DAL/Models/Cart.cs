using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Cart
    {
        [Key]
        public Guid cartID { get; set; }

        // Foreign key to associate the cart with a user
        public Guid userId { get; set; } 
        
        // Navigation property for the user associated with the cart
        public User user { get; set; }

        // Collection of items in the cart (association with Product)
        public ICollection<CartItem> CartItems { get; set; }
        
    }
}
