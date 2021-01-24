using System;
using RabbitMQ.Client;
using System.Text;

namespace Sample.Client
{
    public class MessageService
    {

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "blackbox", UserName = "admin", Password = "F00maniac61", Port = 5672 };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "messageQueue",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "messageQueue",
                                         basicProperties: null,
                                         body: body);
                }
            }
        }
    }
}
