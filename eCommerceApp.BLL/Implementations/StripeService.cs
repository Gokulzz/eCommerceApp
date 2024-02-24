using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.BLL.Stripe;
using eCommerceApp.DAL.Models;
using eCommerceApp.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace eCommerceApp.BLL.Implementations
{
    
    public class StripeService : IStripeService
    {
        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserService _userService;

        public StripeService(
            TokenService tokenService,
            CustomerService customerService,
            ChargeService chargeService,
            IUnitofWork unitofWork,
            IUserService userService)
        {
            _tokenService = tokenService;
            _customerService = customerService;
            _chargeService = chargeService;
            _unitofWork = unitofWork;
            _userService = userService;
            
        }

        public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
        {
            try
            {

                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name = resource.card.Name,
                        Number = resource.card.Number,
                        ExpYear = resource.card.ExpiryYear,
                        ExpMonth = resource.card.ExpiryMonth,
                        Cvc = resource.card.Cvc
                    }

                };

                var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

                var customerOptions = new CustomerCreateOptions
                {
                    Email = resource.Email,
                    Name = resource.Name,
                    Source = token.Id
                };
                var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);
                var paymentMethod = new CustomerPaymentMethod()
                {
                    userId = _userService.GetCurrentId(),
                    stripeCustomerId = customer.Id,
                    expMonth = resource.card.ExpiryMonth,
                    expYear = resource.card.ExpiryYear,
                    Type = "Credit",
                    Brand = "Visa",
                    Last4 = resource.card.Number.Substring(resource.card.Number.Length - 4, 4)


                };
                await _unitofWork.PaymentMethodRepository.PostAsync(paymentMethod);
                await _unitofWork.Save();


                return new CustomerResource(customer.Id, customer.Email, customer.Name);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
           
        }

        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource,PaymentDTO paymentDTO, CancellationToken cancellationToken)
        {
            if (resource == null)
            {
               
                throw new ArgumentNullException(nameof(resource));
            }
            long payment_Amount = (long)Math.Round((paymentDTO.amount * 100));
            var chargeOptions = new ChargeCreateOptions
            {
                Currency = resource.Currency,
                Amount = payment_Amount,
                ReceiptEmail = resource.ReceiptEmail,
                Customer = resource.CustomerId,
                Description = resource.Description
            };
            var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);
            var payment = new Payment()
            {
                Amount = paymentDTO.amount,
                paymentDate = DateTime.Now,
                Status = "Paid",
                paymentMethodId = paymentDTO.paymentMethodId,
                orderId = paymentDTO.orderId
            };
            await _unitofWork.PaymentRepository.PostAsync(payment);
            await _unitofWork.Save();

            return new ChargeResource(
                charge.Id,
                charge.Currency,
                charge.Amount,
                charge.CustomerId,
                charge.ReceiptEmail,
                charge.Description);
        }
    }
}
