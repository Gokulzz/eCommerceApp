using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Repository;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using eCommerceApp.BLL.DTO;

namespace eCommerceApp.BLL.Implementations
{
    public class ConsumeMessage : IConsumeMessage
    {
        private readonly IUserService userService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IUnitofWork unitofWork;
        public ILogger<ConsumeMessage> Logger { get; set; }
        public ConsumeMessage(IUserService userService, IEmailSenderService emailSenderService, IUnitofWork unitofWork, ILogger<ConsumeMessage> Logger)
        {
            this.userService = userService;
            this.emailSenderService = emailSenderService;
            this.unitofWork = unitofWork;
            this.Logger = Logger;
        }
        public async void Subscribe()
        {
            var userId = userService.GetCurrentId();
            var userEmail = await unitofWork.UserRepository.GetEmail(userId);
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var sendMessage = new MessageDTO(new string[] { userEmail }, "Order placed successfully", message);
                Logger.LogInformation(message);
                await emailSenderService.SendEmailAsync(sendMessage);
            };
            channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
            Console.ReadLine();

        }
    }
}

