using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId = Guid.NewGuid();
        public string Role_Name { get; set; }
        public string Role_Description { get; set; }
        public ICollection<User> users { get; set; }
    }
}
