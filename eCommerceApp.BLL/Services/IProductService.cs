﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IProductService
    {
        public Task<ApiResponse> GetAllProducts();
        public Task<ApiResponse> GetProduct(Guid productId);  
        public Task<ApiResponse> AddProduct(ProductDTO product);
        public Task<ApiResponse> UpdateProduct(ProductDTO product); 
        public Task<ApiResponse> DeleteProduct(Guid productId); 
        
    }
}
    