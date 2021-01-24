using System;
using RabbitMQ.Client;
using MessageQueue.Interfaces;

namespace MessageQueue.Handler
{
    public class MessageProducer : IMessageProducer, IDisposable
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly string _exchangeName;

        public MessageProducer(ConnectionSettings connectionSettings)
        {    
            var factory = new ConnectionFactory
            {
                HostName = connectionSettings.HostName,
                Port = connectionSettings.PortNumber
            };

            _exchangeName = connectionSettings.ExchangeName;

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, "topic", true, false, null);
        }

        public void Publish(byte[] message)
        {
            var exchangeName = _exchangeName;
            IBasicProperties basicProperties = _channel.CreateBasicProperties();
            _channel.BasicPublish(exchangeName, "", basicProperties, message);
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
