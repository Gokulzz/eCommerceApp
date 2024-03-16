using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using FluentValidation.Results;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace eCommerceApp.BLL.Implementations
{
    public class ProductService: IProductService
    {
        public IUnitofWork unitofWork { get; set; }
        public IUserService userService { get; set; }  
        public ILogger<Product> logger { get; set; }
        public IWebHostEnvironment webHost { get; set; }
        public IMapper mapper { get; set; }
        private readonly IDistributedCache cache;
        private const string cacheKey = "eCommerce";
        public ProductService(IUnitofWork unitofWork, IMapper mapper, IUserService userService, IWebHostEnvironment webHost, IDistributedCache cache, ILogger<Product> logger)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.userService = userService;
            this.webHost = webHost;
            this.cache = cache;
            this.logger = logger;
        }
        public async Task<ApiResponse> GetAllProducts()
        {
            List<Product> product = new List<Product>();
            List<ProductResultDTO> map_product= new List<ProductResultDTO>();
            var cachedData= await cache.GetAsync(cacheKey);
            if (cachedData == null)
            { 
                logger.LogInformation("Fetching the data from the database and populating cache");
                product= await unitofWork.ProductRepository.GetAllAsync();
                map_product = mapper.Map<List<ProductResultDTO>>(product);
                var cacheDataString= JsonConvert.SerializeObject(map_product);
                var cacheDataBytes= Encoding.UTF8.GetBytes(cacheDataString);
                var caching = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                await cache.SetAsync(cacheKey, cacheDataBytes, caching);

            }
            else
            {
                logger.LogInformation("Fetching the data from the redis cache");
                var stringData= Encoding.UTF8.GetString(cachedData);
                 map_product= JsonConvert.DeserializeObject<List<ProductResultDTO>>(stringData); 
                

            }
           
            return new ApiResponse(200, "All the products returned successfully",map_product ); 
        }
        public async Task<ApiResponse> GetProduct(Guid Id)
        {
            var product = await unitofWork.ProductRepository.GetAsync(Id);
            var map_product= mapper.Map<ProductResultDTO>(product);
            return new ApiResponse(200, $"Product of {Id} returned successfuly", map_product);

        }
        //I have to fix the conversion of product name into lowercase fix the logic.............
        public async Task<ApiResponse> GetByName(string productName)
        {
            var product= await unitofWork.ProductRepository.GetProductByName(productName);
            if(product==null)
            {
                throw new NotFoundException("Could not found product");
            }
            return new ApiResponse(200, $"Product of {productName} returned successfully", product);
        }
        public async Task<PagedResponse> GetPagedData(PaginationFiltersDTO paginationFiltersDTO)
        {
            try
            {
                var filter = mapper.Map<PaginationFilters>(paginationFiltersDTO);
                var count_Records =await  unitofWork.ProductRepository.productCount() ;
                var totalPages = (int)Math.Ceiling(count_Records / (decimal)filter.PageSize);
                var result = await unitofWork.ProductRepository.GetPagedData(filter);
                return new PagedResponse(200, "data displayed successfully", result, filter.PageNumber, filter.PageSize, totalPages,
                 count_Records);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ApiResponse> AddProduct(ProductDTO productDTO)
        {
            var path = webHost.WebRootPath;
            var filePath = "Content/Files/" + productDTO.fileData.FileName;
            var fullPath = Path.Combine(path, filePath);
            await productDTO.fileData.CopyToAsync(new FileStream(fullPath, FileMode.Create));
            var add_product = mapper.Map<Product>(productDTO);
            add_product.userId = userService.GetCurrentId();
            add_product.FilePath = filePath;
            var productCategory = new ProductCategory()
            {
                productsproductId = add_product.productId,
                CategoriescategoryId = productDTO.CategoryId
            };
            await unitofWork.ProductRepository.PostAsync(add_product);
            await unitofWork.ProductCategoryRepository.PostAsync(productCategory);
            await unitofWork.Save();
            return new  ApiResponse(200, "New Product Added successfully", add_product);
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
        public async Task<double> GetProductprice(Guid productId)
        {
            var product= await unitofWork.ProductRepository.GetProductprice(productId);
            return product;
        }

    }


}

