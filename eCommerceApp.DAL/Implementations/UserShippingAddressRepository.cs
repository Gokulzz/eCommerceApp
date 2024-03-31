using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.DAL.Implementations
{
    public class UserShippingAddressRepository : GenericRepository<UserShippingAddress>, IUserShippingAddressRepository
    {
        public UserShippingAddressRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
