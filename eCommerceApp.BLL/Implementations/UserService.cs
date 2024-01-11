using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApp.BLL.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUnitofWork unitofWork;
        private readonly IValidator<UserDTO> validator;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccessor;
        public UserService(IUnitofWork unitofWork, IValidator<UserDTO> validator, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            this.unitofWork = unitofWork;
            this.validator = validator;
            this.configuration = configuration;
            this.contextAccessor = contextAccessor;
        }
        public async Task<ApiResponse> GetAllUser()
        {
            var users = await unitofWork.UserRepository.GetAllAsync();
            return new ApiResponse(200, "User Displayed successfully", users);

        }
        public async Task<ApiResponse> GetUser(Guid id)
        {
            var find_id = await unitofWork.UserRepository.GetAsync(id);
            return new ApiResponse(200, $"User of {id} displayed successfully", find_id);
        }
        public async Task<ApiResponse> AddUser(UserDTO userDTO)
        {
            try
            {
                var validate_user = validator.Validate(userDTO);
                if (validate_user.IsValid)
                {
                    CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordsalt);
                    var add_User = new User
                    {
                        UserName = userDTO.userName,
                        Email = userDTO.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordsalt,
                        Address= userDTO.Address,
                        VerificationToken = GenerateToken(),
                        roleId = userDTO.roleId

                    };
                    await unitofWork.UserRepository.PostAsync(add_User);
                    await unitofWork.Save();
                    return new ApiResponse(200, "Need to enter the verification token send to your email to complete the process of Registration", add_User.VerificationToken);
                }
                else
                {
                    throw new BadRequestException(validate_user.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }
        public async Task<ApiResponse> DeleteUser(Guid id)
        {
            var find_User = await unitofWork.UserRepository.DeleteAsync(id);
            if (find_User == null)
            {
                throw new NotFoundException($"User of this {id} could not be found");
            }
            return new ApiResponse(200, $"User of id= {id} deleted successfully", find_User);
        }
        public async Task<ApiResponse> UpdateUser(Guid id, UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> VerifyUser(Guid Token)
        {
            var user = await unitofWork.UserRepository.VerifyUser(Token);
            user.VerifiedAt = DateTime.Now;
            await unitofWork.Save();
            return new ApiResponse(200, "User Verified Successfully", user);

        }
        public async Task<ApiResponse> LoginUser(UserLoginDTO userLoginDTO)
        {
            var search_User = await unitofWork.FindUserByEmail(userLoginDTO.Email);
            if (search_User == null)
            {
                throw new NotFoundException($"User of email= {userLoginDTO.Email} could not be found");
            }
            List<Claim> Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, search_User.UserName),
                new Claim(ClaimTypes.Role, search_User.RoleName),
                new Claim(ClaimTypes.NameIdentifier, search_User.userId.ToString())
            };
            if (GetPasswordHash(userLoginDTO.Password, search_User.PasswordHash, search_User.PasswordSalt))
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT").GetSection("SecretKey").Value));
                var SigningCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512);
                var token = new JwtSecurityToken(
                    issuer: configuration.GetSection("JWT").GetSection("ValidIssuer").Value,
                    audience: configuration.GetSection("JWT").GetSection("ValidAudience").Value,
                    claims: Claims,

                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: SigningCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return new ApiResponse(201, "JWT Token generated successfully", tokenString);

            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        /*public async Task<Guid> FindUserbyName(string username)
        {
            var find_user = await unitofWork.userRepository.FindUserByName(username);
            return find_user;
        }*/
        public Guid GetCurrentId()
        {
            //we are using httpcontext accessor as we are trying to access the http context inside  the service
            //we are getting the id of the authenticated user from the JWT token.
            var claimIdentity = contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (claimIdentity != null)
            {
                var userIdClaim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }
                throw new UnauthorizedAccessException("UserId not found");


            }
            else
            {
                throw new NotFoundException("UserId of this claim could not be found");
            }

        }

        public static void CreatePasswordHash(string Password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }

        }
        public static bool GetPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return computeHash.SequenceEqual(computeHash);
            }
        }

        public Guid GenerateToken()
        {
            Guid token = Guid.NewGuid();
            return token;
        }
    }

}


