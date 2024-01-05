using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Implementations;
using eCommerceApp.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigureManager
{
    public static class ConfigureDependencies
    {
        public static void ConfigureDependency(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();    
            services.AddScoped<IOrderdetailRepository,OrderDetailRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();    
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
        }

    }
}
