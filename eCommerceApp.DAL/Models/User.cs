using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class User
    {

        [Key]
        public Guid userId = Guid.NewGuid();
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserName { get; set; }
        public Guid VerificationToken { get; set; }
        public Guid roleId { get; set; }
        public Role role { get; set; }
        //User can order many products
        public Cart cart { get; set; }
        public Guid cartId { get; set; }

        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }
        //A single user can have many orders
        public ICollection<Order> Orders { get; set; }
        //A single user can have multiple paymentMethods
        public ICollection<CustomerPaymentMethod> paymentmethods { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? RoleName { get; set; }   
        
       
        
        
    }
}
