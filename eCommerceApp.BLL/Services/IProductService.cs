using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace eCommerceApp.BLL.Services
{
    public interface IProductService
    {
        public Task<ApiResponse> GetAllProducts();
        public Task<ApiResponse> GetProduct(Guid productId);
        public Task<PagedResponse> GetPagedData(PaginationFiltersDTO paginationFiltersDTO);
        public Task<ApiResponse> GetByName(string productName);
        public Task<ApiResponse> AddProduct(ProductDTO product);
        public Task<ApiResponse> UpdateProduct(ProductDTO product); 
        public Task<ApiResponse> DeleteProduct(Guid productId);
        public  Task<double> GetProductprice(Guid productId);


    }
}
    