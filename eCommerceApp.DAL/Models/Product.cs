﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Product
    {
        [Key]
        public Guid productId = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } = 0;
        public Guid userId { get; set; }
        public User user { get; set; }

        public string FilePath { get; set; }
        
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
