using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Category
    {
        [Key]
        public Guid categoryId = new Guid();
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
