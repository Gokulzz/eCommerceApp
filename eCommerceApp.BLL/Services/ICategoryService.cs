using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;
using Microsoft.Identity.Client;

namespace eCommerceApp.BLL.Services
{
    public interface ICategoryService
    {
        public Task<ApiResponse> GetCategory(Guid id);
        public Task<ApiResponse> GetAllCategory();
        public Task<ApiResponse> AddCategory(CategoryDTO category);
        public Task<ApiResponse> DeleteCategory(Guid id);
        public Task<ApiResponse> UpdateCategory(CategoryDTO category);
    }
}