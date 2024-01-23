using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using FluentValidation.Results;
using Microsoft.Identity.Client;

namespace eCommerceApp.BLL.Implementations
{
    public class ProductService: IProductService
    {
        public IUnitofWork unitofWork { get; set; }
        public IUserService userService { get; set; }   
        public IMapper mapper { get; set; } 
        public ProductService(IUnitofWork unitofWork, IMapper mapper, IUserService userService) { 
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService= userService;
        }
        public async Task<ApiResponse> GetAllProducts()
        {
            var all_Products= await unitofWork.ProductRepository.GetAllAsync();
            return new ApiResponse(200, "All the products returned successfully", all_Products);
        }
        public async Task<ApiResponse> GetProduct(Guid Id)
        {
            var product = await unitofWork.ProductRepository.GetAsync(Id);
            return new ApiResponse(200, $"Product of {Id} returned successfuly", product);

        }
        public async Task<ApiResponse> AddProduct(ProductDTO productDTO)
        {
            
            var add_product = mapper.Map<Product>(productDTO);
            add_product.userId = userService.GetCurrentId();
            var productCategory = new ProductCategory()
            {
                productsproductId = add_product.productId,
                CategoriescategoryId = productDTO.CategoryId
            };
            await unitofWork.ProductRepository.PostAsync(add_product);
            await unitofWork.ProductCategoryRepository.PostAsync(productCategory);
            await unitofWork.Save();
            return new ApiResponse(200, "New Product Added successfully", add_product);
        }
        public async Task<ApiResponse> UpdateProduct(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> DeleteProduct(Guid productId)
        {

            var delete_product = await unitofWork.ProductRepository.DeleteAsync(productId);
            await unitofWork.Save();
            return new ApiResponse(200, "Category deleted successsfully", delete_product);


        }
    }


}

