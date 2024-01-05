using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.DAL.Implementations
{
    public class UnitofWork : IUnitofWork
    {
        public readonly DataContext dataContext;
        public IUserRepository UserRepository { get;  } 
        public IRoleRepository RoleRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; } 
        public IOrderdetailRepository OrderdetailRepository { get; } 
        public ICartRepository CartRepository { get; }
        public ICartItemRepository CartItemRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public UnitofWork(DataContext dataContext)
        {
            this.dataContext = dataContext; 
            UserRepository= new UserRepository(dataContext);
            RoleRepository= new RoleRepository(dataContext);
            ProductRepository= new ProductRepository(dataContext);
            OrderRepository= new OrderRepository(dataContext);
            OrderdetailRepository= new OrderDetailRepository(dataContext);
            CartRepository= new CartRepository(dataContext);
            CartItemRepository= new CartItemRepository(dataContext);
            CategoryRepository= new CategoryRepository(dataContext);
            PaymentRepository= new PaymentRepository(dataContext);  
            PaymentMethodRepository= new PaymentMethodRepository(dataContext);
            
        }
        public async Task Save()
        {
            await dataContext.SaveChangesAsync();
        }

       
    }
}
