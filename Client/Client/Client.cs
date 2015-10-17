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
            byte[] buffer = new byte[1024];


            try
            {
                
                IPAddress ip = IPAddress.Parse(settings.ip_address);
                IPEndPoint remoteEP = new IPEndPoint(ip, settings.port_number);

                try
                {
                    Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                    sender.Connect(remoteEP);

                    Console.WriteLine("Connected to " + sender.RemoteEndPoint.ToString());

                    byte[] msg = Encoding.ASCII.GetBytes("This is a sin<EOF>");

                    int sent = sender.Send(msg);

                    int bytesRec = sender.Receive(buffer);

                    Console.WriteLine("echo = " + Encoding.ASCII.GetString(buffer, 0, bytesRec));

                    buffer = new byte[1024];
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();




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
