using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Models
{
    public class Payment
    {

        [Key]
        public Guid paymentId = new Guid();
        public int Amount { get; set; }
        public DateTime paymentDate { get; set; }
        public string Status { get; set; }
        public Guid paymentMethodId { get; set; }
        // A single payment is associated with single payment method.
        public Paymentmethod paymentmethod { get; set; }
        
        

    }
}
