using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.DAL.Repository
{
    public interface IUnitofWork
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderdetailRepository OrderdetailRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IShippingAddressRepository ShippingAddressRepository { get; }
        public IUserShippingAddressRepository UserShippingAddressRepository { get; }
        public Task Save();
        public  Task<User> FindUserByEmail(string email);
       
    }
}
