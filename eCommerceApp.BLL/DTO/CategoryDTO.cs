using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.BLL.DTO
{
    public class CategoryDTO
    {
        public Guid categoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        //public ICollection<Product> products { get; set; }
    }
}
