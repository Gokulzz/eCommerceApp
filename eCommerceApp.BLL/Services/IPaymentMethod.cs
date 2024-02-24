﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Services
{
    public interface IPaymentMethod
    {
        public Task<ApiResponse> GetAllPaymentMethod();
        public Task<ApiResponse> AddPaymentMethod();
    }
}
