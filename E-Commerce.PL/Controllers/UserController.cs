using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL;
using eCommerceApp.BLL.Implementations;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.PL.Controllers
{
    //[Authorize]
    
    public class UserController : Controller
    {
        public IUserService UserService { get; set; }
        public UserController(IUserService UserService)
        {
            this.UserService =UserService;
        }
        [HttpGet("GetUser")]
        public async Task<ApiResponse> GetUser(Guid id)
        {
            var getUser = await UserService.GetUser(id);
            return getUser;
        }
        [HttpGet("GetByEmail")]
        public async Task<ApiResponse> GetByEmail(string email)
        {
            var user= await UserService.GetUserByEmail(email);
            return user;
        }

        [HttpGet("GetAllUser")]
        public async Task<ApiResponse> GetAllUser()
        {
            var getUsers = await UserService.GetAllUser();
            return getUsers;
        }
        [HttpPost("AddUser")]
        public async Task<ApiResponse> AddUser([FromBody] UserDTO user)
        {
            var addUser = await UserService.AddUser(user);
            return addUser;
        }
        [HttpPut("UpdateUsers")]
        public async Task<ApiResponse> UpdateUsers(Guid Id, UserDTO user)
        {
            var update_User = await UserService.UpdateUser(Id, user);
            return update_User;
        }
        [HttpDelete("DeleteUsers")]
        public async Task<ApiResponse> DeleteUsers(Guid id)
        {
            var delete_User = await UserService.DeleteUser(id);
            return delete_User;
        }
       
        [HttpPost("VerifyUser")]
        public async Task<ApiResponse> VerifyUser([FromBody] Guid token)
        {
            var verify_User = await UserService.VerifyUser(token);
            return verify_User;
        }
    }
}

