using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(MessageDTO message);    
    }
}
