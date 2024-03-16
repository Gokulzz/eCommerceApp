﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IUserService
    {
        public Task<ApiResponse> GetAllUser();
        public Task<ApiResponse> GetUser(Guid Id);
        public Task<ApiResponse> GetUserByEmail(string Email);  
        public Task<ApiResponse> AddUser(UserDTO userDTO);
        public Task<ApiResponse> UpdateUser(Guid Id, UserDTO userDTO);
        public Task<ApiResponse> DeleteUser(Guid Id);
        public Task<ApiResponse> VerifyUser(Guid VerificationToken);
        public Task<ApiResponse> LoginUser(UserLoginDTO userLogin);
        public Guid GetCurrentId();
    }
}
