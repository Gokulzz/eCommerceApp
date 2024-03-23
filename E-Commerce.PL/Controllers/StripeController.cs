using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Implementations;
using eCommerceApp.BLL.Services;
using eCommerceApp.BLL.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.PL.Controllers
{

    [Route("stripe")]
    public class StripeController : Controller
    {
        public IStripeService stripeService { get; set; }
        public StripeController(IStripeService stripeService)
        {
            this.stripeService = stripeService;
        }
        [HttpPost("Customer")]
        public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource, CancellationToken cancellationToken)
        {
            var response = await stripeService.CreateCustomer(resource, cancellationToken);
            return Ok(response);
        }

        [HttpPost("Charge")]
        public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource,PaymentDTO paymentDTO, CancellationToken cancellationToken)
        {
            var response = await stripeService.CreateCharge(resource, paymentDTO, cancellationToken);
            return Ok(response);
        }
    }
}
