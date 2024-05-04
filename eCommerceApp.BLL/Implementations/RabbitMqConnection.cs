using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Services;
using RabbitMQ.Client;

namespace eCommerceApp.BLL.Implementations
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
    {
        private IConnection? connection;
        public IConnection Connection => connection!;
        public RabbitMqConnection()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            connection = factory.CreateConnection();    

        }
        public void Dispose()
        {
            connection?.Dispose();
        }
    }

}
