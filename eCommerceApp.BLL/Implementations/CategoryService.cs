using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.Identity.Client;

namespace eCommerceApp.BLL.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
        public CategoryService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;

        }
        public async Task<ApiResponse> GetCategory(Guid Id)
        {
            var category = await unitofWork.CategoryRepository.GetAsync(Id);
            if (category == null)
            {
                throw new NotFoundException($"Category of Id= {Id} could not be found");
            }
            return new ApiResponse(200, "Category returned successfully", category);
        }
        public async Task<ApiResponse> GetAllCategory()
        {
            var categories = await unitofWork.CategoryRepository.GetAllAsync();
            if (categories == null)
            {
                throw new NotFoundException("No categories found");
            }
            return new ApiResponse(200, "Categories returned successfully", categories);
        }
        public async Task<ApiResponse> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var map_category = mapper.Map<Category>(categoryDTO);
                await unitofWork.CategoryRepository.PostAsync(map_category);
                await unitofWork.Save();
                return new ApiResponse(200, "New category added successfully", map_category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }
        public async Task<ApiResponse> UpdateCategory(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> DeleteCategory(Guid id)
        {
            try
            {
                var delete_category = await unitofWork.CategoryRepository.DeleteAsync(id);
                await unitofWork.Save();    
                return new ApiResponse(200, "Category deleted successsfully", delete_category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
       

    

