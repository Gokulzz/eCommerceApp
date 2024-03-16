using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eCommerceApp.BLL.DTO
{
    public class ProductResultDTO
    {
        public Guid productId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string filePath { get; set; }
       
    }

    
}
