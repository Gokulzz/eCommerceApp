using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<double> GetProductprice(Guid id);
        public  Task<int> productCount();
        public Task<int> getProductQuantity(Guid id);
        
        public  Task<List<Product>> GetPagedData(PaginationFilters paginationFilters);
        public Task<Product> GetProductByName(string productName);  

    }
}
