using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class CartItem
    {
        public Guid CartItemID { get; set; }

        // Foreign key to associate the cart item with a product
        public Guid ProductID { get; set; }

        // Navigation property for the product associated with the cart item
        public Product Product { get; set; }

        // Quantity of the product in the cart
        public int Quantity { get; set; }

        // Foreign key to associate the cart item with a cart
        public Guid CartID { get; set; }
        

        // Navigation property for the cart associated with the cart item
        public Cart Cart { get; set; }
      
    }
}
