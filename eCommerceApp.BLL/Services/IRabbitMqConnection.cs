using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace eCommerceApp.BLL.Services
{
   public interface IRabbitMqConnection
    {
        public IConnection Connection { get; }
    }
}
