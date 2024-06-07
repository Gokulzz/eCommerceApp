using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.DAL.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            
        }
        public async Task<double> GetProductprice(Guid id)
        {
            var find_product= await dataContext.Products.FindAsync(id);
            return find_product.Price;

            
        }
        public async Task<int> productCount()
        {
            return dataContext.Products.Count();    
        }
        public async Task<List<Product>> GetPagedData(PaginationFilters paginationFilters)
        {
            var validFilters= new PaginationFilters(paginationFilters.PageNumber, paginationFilters.PageSize);
            var pagedData = await dataContext.Products.Skip((validFilters.PageNumber - 1) * (validFilters.PageSize)).Take(validFilters.PageSize)
                .ToListAsync();
            return pagedData;
        }
        public async Task<Product> GetProductByName(string productName)
        {
            var product= await dataContext.Products.Where(x=>x.Name==productName).FirstOrDefaultAsync();
            return product;
        }
        public async Task<int> getProductQuantity(Guid id)
        {
            var get_Product= await dataContext.Products.Where(x=>x.productId==id).FirstOrDefaultAsync();
            return get_Product.Quantity;
        }
    }
    
}
