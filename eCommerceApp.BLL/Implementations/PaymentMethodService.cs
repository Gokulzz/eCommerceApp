using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Repository;

namespace eCommerceApp.BLL.Implementations
{
    public class PaymentMethodService : IPaymentMethod
    {
        public IUnitofWork unitofWork { get; set; }   
        public PaymentMethodService(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        public Task<ApiResponse> GetAllPaymentMethod()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> AddPaymentMethod()
        {
            throw new NotImplementedException();
        }
    }
}
