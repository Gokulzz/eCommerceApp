using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IRoleService
    {

        public Task<ApiResponse> GetAllRole();
        public Task<ApiResponse> GetRole(Guid Id);
        public Task<ApiResponse> AddRole(RoleDTO roleDTO);
        public Task<ApiResponse> UpdateRole(Guid Id, RoleDTO roleDTO);
        public Task<ApiResponse> DeleteRole(Guid Id);
    }
}
