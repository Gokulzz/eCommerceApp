using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using eCommerceApp.BLL.Implementations;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.PL.Controllers
{
    
    public class ProductController : Controller
    {
        public IProductService productService { get; set; } 
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<ApiResponse> GetProductById(Guid id)
        {
            var category = await productService.GetProduct(id);
            return category;
        }
        [HttpGet("GetProductByName")]
        public async Task<ApiResponse> GetProductByName(string productName)
        {
            var product= await productService.GetByName(productName);
            return product;
        }
        [HttpGet("GetAllProduct")]
        public async Task<ApiResponse> GetAllProduct()
        {
            var allCategory = await productService.GetAllProducts();
            return allCategory;
        }
        [HttpGet("GetPagedProducts")]
        public async Task<ApiResponse> GetPagedProducts(PaginationFiltersDTO paginationFiltersDTO)
        {
            var getData = await productService.GetPagedData(paginationFiltersDTO);
            return getData;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public async Task<ApiResponse> AddProduct( ProductDTO product)
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

