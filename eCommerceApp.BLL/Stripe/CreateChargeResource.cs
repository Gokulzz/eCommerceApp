using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Stripe
{
    public class CreateChargeResource
    {
      public string Currency { get; set; }    
      public long Amount { get; set; } 
      public string CustomerId { get; set; }    
      public string ReceiptEmail { get; set; }    
      public string Description { get; set; }   
    }
}
