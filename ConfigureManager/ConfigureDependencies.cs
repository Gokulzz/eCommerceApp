using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using eCommerceApp.BLL.Implementations;
using eCommerceApp.BLL.Services;
using eCommerceApp.BLL.Validations;
using eCommerceApp.DAL.Data;
using eCommerceApp.DAL.Implementations;
using eCommerceApp.DAL.Repository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using PaymentMethodService = eCommerceApp.BLL.Implementations.PaymentMethodService;

namespace ConfigureManager
{
    public static class ConfigureDependencies
    {
        public static void ConfigureDependency(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddHttpContextAccessor();
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
            services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IUserShippingAddressRepository, UserShippingAddressRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IProductService, eCommerceApp.BLL.Implementations.ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();  
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();  
            services.AddScoped<IShippingAddressService, ShippingAddressService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
            services.AddScoped<IMessageProducer, RabbitMQProducer>(); 
            services.AddScoped<TokenService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<ChargeService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IConsumeMessage, ConsumeMessage>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddControllers().AddJsonOptions(x =>
              x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);





        }

    }
}
