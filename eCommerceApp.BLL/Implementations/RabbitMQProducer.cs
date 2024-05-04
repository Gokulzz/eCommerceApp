using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Services;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using RabbitMQ.Client;

namespace eCommerceApp.BLL.Implementations
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IRabbitMqConnection connection;
        public RabbitMQProducer(IRabbitMqConnection connection)
        {
            this.connection = connection;
        }

        public void SendMessage<T>(T message)
        {
            try
            {

                using var channel = connection.Connection.CreateModel();
                channel.QueueDeclare("orders", exclusive: false) ;
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(message, jsonSerializerSettings);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw;
            }
        }
    }

}
