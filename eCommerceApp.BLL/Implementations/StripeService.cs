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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Stripe;

namespace eCommerceApp.BLL.Implementations
{

    public class StripeService : IStripeService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;
        private readonly IUserService _userService;
        private readonly IMessageProducer _messageProducer;
        private readonly IConsumeMessage _consumeMessage;

        public StripeService(
            IServiceScopeFactory serviceScopeFactory,
            TokenService tokenService,
            CustomerService customerService,
            ChargeService chargeService,
            IUserService userService,
            IMessageProducer messageProducer,
            IConsumeMessage consumeMessage)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _tokenService = tokenService;
            _customerService = customerService;
            _chargeService = chargeService;
            _userService = userService;
            _messageProducer = messageProducer;
            _consumeMessage = consumeMessage;
        }

        public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

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

                await unitOfWork.PaymentMethodRepository.PostAsync(paymentMethod);
                await unitOfWork.Save();

                return new CustomerResource(customer.Id, customer.Email, customer.Name);
            }
        }

        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

                if (resource == null)
                {
                    throw new ArgumentNullException(nameof(resource));
                }

                var userId = _userService.GetCurrentId();
                var getEmail = await unitOfWork.UserRepository.GetEmail(userId);
                var orderId = await unitOfWork.OrderRepository.GetOrderId(userId);
                var cartId = await unitOfWork.CartRepository.GetCartId(userId);
                var grandTotal = await unitOfWork.OrderRepository.GetOrderAmount(orderId);
                var paymentMethodId = await unitOfWork.PaymentMethodRepository.GetPaymentMethodId(userId);
                var getStripeId = await unitOfWork.PaymentMethodRepository.GetAsync(paymentMethodId);
                var order = await unitOfWork.OrderRepository.GetAsync(orderId);
                var cart = await unitOfWork.CartRepository.GetAsync(cartId);
                long payment_Amount = (long)Math.Round((grandTotal * 100));

                var chargeOptions = new ChargeCreateOptions
                {
                    Currency = resource.Currency,
                    Amount = payment_Amount,
                    ReceiptEmail = getEmail,
                    Customer = getStripeId.stripeCustomerId
                };

                var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

                var payment = new Payment()
                {
                    Amount = grandTotal,
                    paymentDate = DateTime.Now,
                    Status = "Paid",
                    paymentMethodId = paymentMethodId,
                    orderId = orderId
                };

                await unitOfWork.PaymentRepository.PostAsync(payment);
                order.Status = "Placed";
                cart.cartCheckout = "Yes";
                await unitOfWork.OrderRepository.UpdateAsync(orderId, order);
                await unitOfWork.CartRepository.UpdateAsync(cartId, cart);
                await unitOfWork.Save();

                _consumeMessage.Subscribe();
                await UpdateProductInfo(cartId);

                return new ChargeResource(
                    charge.Id,
                    charge.Currency,
                    charge.Amount,
                    charge.CustomerId,
                    charge.ReceiptEmail
                );
            }
        }

        public async Task UpdateProductInfo(Guid cartId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

                var products = await unitOfWork.CartItemRepository.GetIDandQuantity(cartId);
                foreach (var product in products)
                {
                    var get_Quantity = await unitOfWork.ProductRepository.getProductQuantity(product.ProductID);
                    var get_Product = await unitOfWork.ProductRepository.GetAsync(product.ProductID);
                    get_Product.Quantity = get_Quantity - product.Quantity;
                    await unitOfWork.ProductRepository.UpdateAsync(product.ProductID, get_Product);
                }
                await unitOfWork.Save();
            }
        }
    }

}
