using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace eCommerceApp.DAL.Implementations
{
    public class UnitofWork : IUnitofWork, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IServiceScope _scope;
        private readonly DataContext _context;

        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderdetailRepository OrderdetailRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IShippingAddressRepository ShippingAddressRepository { get; }
        public IUserShippingAddressRepository UserShippingAddressRepository { get; }

        public UnitofWork(IServiceScopeFactory serviceScopeFactory)
        {
            //we are using IServiceScopefactorry to provide a scoped lifetime for the service 
            //we use iservicescope to manage the scope of the current service, ensuring they are properly disposed, using this scopeservice, we get a scoped datacontext instance
            //i.e we are creating a different instance of datacontext for each unitofwork instance to resolve the thread safety problem asssociated with Ef dbcontext.
            _serviceScopeFactory = serviceScopeFactory;
            _scope = _serviceScopeFactory.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<DataContext>();

            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
            OrderdetailRepository = new OrderDetailRepository(_context);
            CartRepository = new CartRepository(_context);
            CartItemRepository = new CartItemRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            PaymentMethodRepository = new PaymentMethodRepository(_context);
            ProductCategoryRepository = new ProductCategoryRepository(_context);
            ShippingAddressRepository = new ShippingAddressRepository(_context);
            UserShippingAddressRepository = new UserShippingAddressRepository(_context);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    

    public async Task<User> FindUserByEmail(string email)
        {
         
            //we are retrieving the user of same email and also retreiving the role assigned to that user.
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Not found");
            }
            var roles = await RoleRepository.GetAsync(user.roleId);
            user.RoleName = roles.Role_Name;
            return user;

        }
       


    }
}
