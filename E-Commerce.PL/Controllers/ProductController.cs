using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using eCommerceApp.BLL.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public IProductService productService { get; set; } 
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetProduct")]
        public async Task<ApiResponse> GetProduct(Guid id)
        {
            var category = await productService.GetProduct(id);
            return category;
        }
        [HttpGet("GetAllProduct")]
        public async Task<ApiResponse> GetAllProduct()
        {
            var allCategory = await productService.GetAllProducts();
            return allCategory;
        }
        [HttpPost("AddProduct")]
        public async Task<ApiResponse> AddProduct(ProductDTO product)
        {
            var addCategory = await productService.AddProduct(product);
            return addCategory;

        }
        [HttpPut("UpdateProduct")]
        public async Task<ApiResponse> UpdateProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }
        [HttpDelete("DeleteProduct")]
        public async Task<ApiResponse> DeleteProduct(Guid id)
        {
            var delete_category = await productService.DeleteProduct(id);
            return delete_category;
        }
    }
}

