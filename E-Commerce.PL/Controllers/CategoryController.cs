using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.BLL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryService service { get; set; }
        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }
        [HttpGet("GetCategory")]
        public async Task<ApiResponse> GetCategory(Guid id)
        {
            var category = await service.GetCategory(id);
            return category;
        }
        [HttpGet("GetAllCategory")]
        public async Task<ApiResponse> GetAllCategory()
        {
            var allCategory = await service.GetAllCategory();
            return allCategory;
        }
        [HttpPost("AddCategory")]
        public async Task<ApiResponse> AddCategory(CategoryDTO category)
        {
            var addCategory = await service.AddCategory(category);
            return addCategory;

        }
        [HttpPut("UpdateCategory")]
        public async Task<ApiResponse> UpdateCategory(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("DeleteCategory")]
        public async Task<ApiResponse> DeleteCategory(Guid id)
        {
            var delete_category = await service.DeleteCategory(id);
            return delete_category;
        }
    }
}
