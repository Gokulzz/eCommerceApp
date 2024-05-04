using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.BLL.Services
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T message);  
    }
}
