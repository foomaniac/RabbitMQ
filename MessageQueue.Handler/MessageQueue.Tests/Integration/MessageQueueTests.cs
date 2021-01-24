using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageQueue.Handler;
using MessageQueue.Interfaces;

namespace MessageQueue.Tests.Integration
{
    [TestClass]
    public class MessageQueueTests
    {
        private IMessageConsumer _messageConsumer;
        private IMessageProducer _messageProducer;

        [TestInitialize]
        public void Init()
        {
            var connectionSettings = new ConnectionSettings()
            { 
                ExchangeName = "Exc.IndexRebuild.Request",
                HostName = "localhost",
                PortNumber = 5670
            };

            _messageProducer = new MessageProducer(connectionSettings);

            _messageConsumer = new MessageConsumer(connectionSettings);
        }

        [TestMethod]
        public void Can_publish_message_to_queue()
        {
            var message = "{CustomerId : 21}";
           
            _messageProducer.Publish(System.Text.Encoding.UTF8.GetBytes(message));
        }

        [TestMethod]
        public void Can_get_message_from_queue()
        {        
            var returnBytes = _messageConsumer.GetMessage("Q.IndexRebuild.Request", "Q.IndexRebuild.Request.Retry10",
                false);

            Assert.IsNotNull(returnBytes);

            var message = System.Text.Encoding.UTF8.GetString(returnBytes);

            Assert.IsNotNull(message);
        }
    }
}
