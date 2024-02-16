using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class CartItemDTO
    {
        public List<CartItemDetailsDTO> items { get; set; } 


        public class CartItemDetailsDTO
        {
            public Guid productId { get; set; }
            public int quantity { get; set; }
        }
       
    }
}
