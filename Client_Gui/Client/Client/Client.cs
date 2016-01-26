using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Client
{
    class Client
    {
        public Entry.ClientRequestInfo settings; /* settings for the user to pass in*/

        /* constructor requires the user to enter in a strucutre with settings.*/
        public Client(Entry.ClientRequestInfo userSettings)
        {
            settings = userSettings;
        }
             
        public void Start()
        {
         
            try
            {
                // get the server and its port and connect it to an endpoint
                IPAddress ip = IPAddress.Parse(settings.ip_address);
                IPEndPoint remoteEP = new IPEndPoint(ip, settings.port_number);

                try
                {
                
                    // socket used to send information
                   Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                   Chat client = new Chat();
                   sender.Connect(remoteEP);

                   // intially we want to send a message to the server telling what IP is connecting to it
                   client.SendIP(sender);                    
             
                   // Begin Chat.
                   while (true)
                   {                      
                       client.SendMessage(sender); // send the message
                       
                       // get the bytes that we recieve
                       byte[] buf = new byte[1024];
                       int BytesRecieved = sender.Receive(buf);

                       // write out any response we recieve.
                       string returned = Encoding.ASCII.GetString(buf, 0, BytesRecieved);
                       returned = returned.Replace("<EOF>", "");
                       Console.WriteLine(returned);

                       
                    
                   }

                   sender.Shutdown(SocketShutdown.Send);
                }
                catch (Exception)
                {
                    
                }

            }
            catch (Exception)
            {                
                throw;
            }

        }
    }
}
