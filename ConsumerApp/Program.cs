using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Repository;
using eCommerceApp.BLL.Implementations;


     
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("orders", exclusive: false);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);

        };
        channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
        Console.ReadLine();
    
