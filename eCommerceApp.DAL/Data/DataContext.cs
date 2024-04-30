using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.userId);
            modelBuilder.Entity<Role>().HasKey(x => x.RoleId);
            modelBuilder.Entity<Cart>().HasKey(x => x.cartID);
            modelBuilder.Entity<CartItem>().HasKey(x => x.CartItemID);
            modelBuilder.Entity<Category>().HasKey(x => x.categoryId);
            modelBuilder.Entity<Order>().HasKey(x => x.orderId);
            modelBuilder.Entity<ShippingAddress>().HasKey(x => x.addressId);
            modelBuilder.Entity<Orderdetails>().HasKey(x => x.orderDetailId);
            modelBuilder.Entity<Payment>().HasKey(x => x.paymentId);
            modelBuilder.Entity<CustomerPaymentMethod>().HasKey(x => x.paymentMethodId);
            modelBuilder.Entity<Product>().HasKey(x => x.productId);
            modelBuilder.Entity<User>()
                .HasOne(x => x.role).WithMany(x => x.users)
                .HasForeignKey(x => x.roleId);
            modelBuilder.Entity<Product>()
                .HasOne(x => x.user).WithMany(x => x.Products)
                .HasForeignKey(x => x.userId);
            modelBuilder.Entity<Order>()
                .HasOne(x=>x.user).WithMany(x => x.Orders)
                .HasForeignKey(x=>x.userId);
            modelBuilder.Entity<CustomerPaymentMethod>()
                .HasOne(x => x.user).WithMany(x => x.paymentmethods)
                .HasForeignKey(x => x.userId);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.products);
            modelBuilder.Entity<Orderdetails>()
                .HasOne(x => x.order).WithMany(x => x.orderDetails)
                .HasForeignKey(x => x.orderId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.paymentmethod).WithMany(x => x.payments)
                .HasForeignKey(x => x.paymentMethodId);
            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Cart).WithMany(x => x.CartItems)
                .HasForeignKey(x => x.CartID);
            modelBuilder.Entity<CartItem>()
                 .HasOne(x => x.Product)
                 .WithMany(x=>x.CartItems)
                 .HasForeignKey(x => x.ProductID)
                   .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasMany(x => x.shippingAddresses)
                .WithMany(x => x.Users)
                .UsingEntity<UserShippingAddress>();
            modelBuilder.Entity<Cart>()
               .HasOne(x => x.user).WithMany(x => x.carts)
               .HasForeignKey(x => x.userId);
            modelBuilder.Entity<Product>()
               .HasMany(x => x.Categories)
               .WithMany(x => x.products)
               .UsingEntity<ProductCategory>();
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.order).WithOne(x => x.payment)
                .HasForeignKey<Payment>(x => x.orderId);
           
        }
        public DbSet<Product> Products { get; set;}
        public DbSet<User> Users { get; set;}   
        public DbSet<Role> Roles { get; set;}   
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderdetails> OrderDetails { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<CustomerPaymentMethod> PaymentMethod { get; set; } 
        public DbSet<Cart> Carts { get; set; }  
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductCategory> CategoryProduct { get; set; } 
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }   
        public DbSet<UserShippingAddress> ShippingAddressUser{ get; set; }
    }
}
