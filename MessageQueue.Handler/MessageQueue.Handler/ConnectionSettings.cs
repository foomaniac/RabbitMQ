using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Handler
{
   public class ConnectionSettings
    {
        public string HostName { get; set; }
        public int PortNumber { get; set; }
        public string ExchangeName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
