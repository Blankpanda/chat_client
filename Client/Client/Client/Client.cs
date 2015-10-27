using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Client.Client
{
    class Client
    {
        public Entry.ClientRequestInfo settings; /* settings for the user to pass in*/

        /* constructor requires the user to enter in a strucutre with settings.*/
        public Client(Entry.ClientRequestInfo userSettings)
        {
            settings = userSettings;
        }
     
        /*template... */
        public void Start()
        {
          //  byte[] buffer = new byte[1024]; // used as a general buffer.


            try
            {
                // get the server and its port and connect it to an endpoint
                IPAddress ip = IPAddress.Parse(settings.ip_address);
                IPEndPoint remoteEP = new IPEndPoint(ip, settings.port_number);

                try
                {
                
                    // socket used to send information
                   Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                   // intially we want to send a message to the server telling what IP is connecting to it
                   sender.Connect(remoteEP);
                   string hostIpAddress = Net.GetHostIpAddress();
                   byte[] msg = Encoding.ASCII.GetBytes(hostIpAddress + " connected. " + "<EOF>");
                   int sent = sender.Send(msg);
                 

                   // Begin Chat.
                   while (true)
                   {

                       Chat client = new Chat();
                       string ChatMessage = "";

                       ChatMessage = client.GetMessageFromStream();
                       byte[] message = Encoding.ASCII.GetBytes(ChatMessage + "<EOF>");

                       sent = sender.Send(message);                      
                   }
                   sender.Shutdown(SocketShutdown.Both);

                }
                catch (Exception)
                {                    
                    throw;
                }

            }
            catch (Exception)
            {                
                throw;
            }

        }
    }
}
