using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Implementations;
using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    public class LoginController : Controller
    {
       private IUserService userService { get; set; }   
       public LoginController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("UserLogin")]
        public async Task<ApiResponse> UserLogin([FromBody] UserLoginDTO userLogin)
        {
            var user_login = await userService.LoginUser(userLogin);
            return user_login;
        }
    }
}
