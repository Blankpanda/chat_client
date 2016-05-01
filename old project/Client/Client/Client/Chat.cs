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

        private string _UserName;

        public Chat()
        {

        }
        public Chat(string Username)
        {
            _UserName = Username;
        }

        /// <summary>
        /// Depracated. dont use this.
        /// </summary>
        /// <returns></returns>
        public string GetMessageFromStream()
        {
            Console.Write(_UserName +":");
            string input = Console.ReadLine();
            return input;
        }

        public int SendMessage(System.Net.Sockets.Socket sender)
        {
            Enum MessageType = Message.MessageType.Message; // the type of message where sending here is a text message.
            Message msg = new Message();

            string sdfsa = GetMessageFromStream();

            byte[] message = Encoding.ASCII.GetBytes(sdfsa + "<EOF>");
            int sent = sender.Send(message);

            return sent;
        }

        public int SendIP(System.Net.Sockets.Socket sender)
        {

            Message msg = new Message();

            string HostIpAddress = 
                Net.GetHostIpAddress();

            // the type of message where sending here is a Ip address.
            string type = 
                "type:" + msg.GetMessageTypeByName(Message.MessageType.SentIP); 
            
            byte[] message = Encoding.ASCII.GetBytes(HostIpAddress + 
                                                " connected. " +
                                                 type + 
                                                  Chat.EOF_FLAG);

            int sent = sender.Send(message);

            return sent;
        }
    }
}
