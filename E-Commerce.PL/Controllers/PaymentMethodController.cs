using eCommerceApp.BLL;
using eCommerceApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{
    [Authorize]
   
    public class PaymentMethodController : Controller
    {
        public IPaymentMethodService PaymentMethodService { get; set; } 
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            this.PaymentMethodService = paymentMethodService;
        }
        [HttpGet("PaymentMethod")]
        public async Task<ApiResponse> PaymentMethod()
        {
            var get_paymentMethod = await PaymentMethodService.GetAllPaymentMethod();
            return get_paymentMethod;
        }
    }
}
