using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Client
{
   
    class Chat
    {
        public const string EOF_FLAG = "<EOF>";
        
        public string GetMessageFromStream()
        {
            Console.Write(":");
            string input = Console.ReadLine();
            return input;
        }

        internal static int SendMessage(System.Net.Sockets.Socket sender)
        {
            string hostIpAddress = Net.GetHostIpAddress();

            byte[] msg = Encoding.ASCII.GetBytes(hostIpAddress + " connected. " + Chat.EOF_FLAG);

            int sent = sender.Send(msg);

            return sent;
        }
    }
}
