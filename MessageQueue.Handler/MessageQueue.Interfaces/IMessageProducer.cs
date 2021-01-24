using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Interfaces
{
    public interface IMessageProducer
    {
        void Publish(byte[] message);
    }
}
