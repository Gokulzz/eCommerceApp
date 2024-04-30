using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Stripe
{
   public class ChargeResource
    {
        public string ChargeId { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public string CustomerId { get; set; }
        public string ReceiptEmail { get; set; }
        //public string Description { get; set; }

        public ChargeResource(string chargeid, string currency, long amount, string customerId, string receiptEmail) { 
            ChargeId= chargeid;
            Currency = currency;
            Amount = amount;
            CustomerId = customerId;
            ReceiptEmail = receiptEmail;
           
        }

        
    }
}
