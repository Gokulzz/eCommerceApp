using System.Text;
using eCommerceApp.BLL.DTO;
using eCommerceApp.BLL.Services;
using eCommerceApp.DAL.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumeMessage
{
    public class EmailNotificationSubscriber
    {
        private readonly IUserService userService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IUnitofWork unitofWork;
        public EmailNotificationSubscriber(IEmailSenderService emailSenderService, IUserService userService, IUnitofWork unitofWork)
        {
            this.emailSenderService = emailSenderService;
            this.userService = userService;
            this.unitofWork = unitofWork;
        }
        public async void Subscribe()
        {
            try { 
            var userId = userService.GetCurrentId();
            var userEmail = await unitofWork.UserRepository.GetEmail(userId);
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                var confirmationMessage = new MessageDTO(new string[] { userEmail }, "Order Placed Successfully", message);
                await emailSenderService.SendEmailAsync(confirmationMessage);
            };
            channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
            Console.WriteLine("Consumed successfully");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
             
        }

    }
}