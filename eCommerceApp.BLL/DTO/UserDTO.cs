﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }   
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
        public string userName { get; set; }    
        public Guid roleId { get; set; }        
    }
}
