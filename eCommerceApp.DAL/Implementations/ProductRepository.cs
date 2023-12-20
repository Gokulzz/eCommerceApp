using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.DAL.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
