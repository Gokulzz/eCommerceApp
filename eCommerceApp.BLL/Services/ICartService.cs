using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.BLL.Services
{
    public interface ICartService
    {
        public  Task<ApiResponse> GetAll();

    }
}
