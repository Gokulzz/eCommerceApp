﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace eCommerceApp.DAL.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
           
        }
        public async Task<User> VerifyUser(Guid token)
        {
            var search_token = await dataContext.Users.Where(x => x.VerificationToken == token).FirstOrDefaultAsync();
            return search_token;
            
        }
        public async Task<string> GetEmail(Guid id)
        {
            var get_Email = await dataContext.Users.Where(x => x.userId == id).FirstOrDefaultAsync();
            return get_Email.Email;
                    
        }
       
     }
}
