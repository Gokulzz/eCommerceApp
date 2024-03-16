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

namespace eCommerceApp.BLL.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IMapper mapper;
        public RoleService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> GetAllRole()
        {
            try
            {
                var get_Roles = await unitofWork.RoleRepository.GetAllAsync();
                //we are mapping each propertu of role to roleDTO 
                var map_Roles = mapper.Map<List<RoleDTO>>(get_Roles);
                return new ApiResponse(200, "Roles displayed successfully", map_Roles);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        public async Task<ApiResponse> GetRole(Guid Id)
        {
            try
            {
                var get_user = await unitofWork.RoleRepository.GetAsync(Id);
                return new ApiResponse(200, $"Role of id={Id} displayed successfully", get_user);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }
        public async Task<ApiResponse> AddRole(RoleDTO roleDTO)
        {
            try
            {
                var map_Roles = mapper.Map<Role>(roleDTO);
                await unitofWork.RoleRepository.PostAsync(map_Roles);
                await unitofWork.Save();
                return new ApiResponse(200, "New Role added successfully", map_Roles);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        }
        public async Task<ApiResponse> UpdateRole(Guid id, RoleDTO roleDTO)
        {
            try
            {
                var find_Role = await unitofWork.RoleRepository.GetAsync(id);
                if (find_Role == null)
                {
                    throw new NotFoundException($"role of this {id} could not be found");
                }
                find_Role.Role_Name = roleDTO.Role_Name;
                find_Role.Role_Description = roleDTO.Role_Description;
                await unitofWork.RoleRepository.UpdateAsync(find_Role);
                await unitofWork.Save();
                return new ApiResponse(200, $"Role of {id} updated successfully", find_Role);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteRole(Guid id)
        {
            try
            {
                var delete_Role = await unitofWork.RoleRepository.DeleteAsync(id);
                await unitofWork.Save();
                return new ApiResponse(200, $"Role of id= {id} is deleted successfully", delete_Role);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

    }

}

