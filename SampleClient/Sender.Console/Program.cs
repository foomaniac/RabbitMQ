using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Client;

namespace Sender
{
   public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var messageService = new MessageService();

                messageService.SendMessage("Hello!!");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message {ex.Message}");
                Console.ReadLine();
            }
            
        }
    }
}
