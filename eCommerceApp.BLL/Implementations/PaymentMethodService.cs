using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Exceptions;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public class PaymentMethodService : IPaymentMethodService
    {
        public IUnitofWork unitofWork { get; set; }
        public IUserService userService { get; set; }   
        public PaymentMethodService(IUnitofWork unitofWork, IUserService userService)
        {
            this.unitofWork = unitofWork;
            this.userService = userService;
        }

        public async Task<ApiResponse> GetAllPaymentMethod()
        {
            var userId= userService.GetCurrentId();
            var get_PaymentMethod = await unitofWork.PaymentMethodRepository.GetPaymentMethod(userId);
            if(get_PaymentMethod == null)
            {
                throw new NotFoundException("Payment method could not be found");
            }
            return new ApiResponse(200, "payment method returned successfully", get_PaymentMethod);
        }

        public Task<ApiResponse> AddPaymentMethod()
        {
            throw new NotImplementedException();
        }
    }
}
