using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MessageQueue.Interfaces;

namespace MessageQueue.Handler
{
    public class MessageConsumer : IMessageConsumer, IDisposable
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly string _exchangeName;

        public MessageConsumer(ConnectionSettings connectionSettings)
        {
            var factory = new ConnectionFactory
            {
                HostName = connectionSettings.HostName,
                Port = connectionSettings.PortNumber
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();           
        }

        public byte[] GetMessage(string queueName, string retryQueueName, bool autoDelete)
        {
            _channel.QueueDeclare(queueName,
                true, 
                false,
                autoDelete, 
                new Dictionary<string, object>()
                {
                    { "x-dead-letter-exchange", retryQueueName }
                });
            
            BasicGetResult result = _channel.BasicGet(queueName, true);

            return result?.Body;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (_connection != null)
            {
                _channel.Close(200, "Goodbye");
                _connection.Close();
                _channel = null;
                _connection = null;
            }
        }
    }
}
