using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buffer = new byte[1024];

            // most of this stuff is going to be gained from user entry.
            try
            {

                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ip = ipHostInfo.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ip, 7777);

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Connected to " + sender.RemoteEndPoint.ToString());

                    byte[] msg = Encoding.ASCII.GetBytes("This is a sin<EOF>");

                    int sent = sender.Send(msg);

                    int bytesRec = sender.Receive(buffer);

                    Console.WriteLine("arko = " + Encoding.ASCII.GetString(buffer,0,bytesRec));
                      
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
            Console.ReadLine();}
        }
    }
}
