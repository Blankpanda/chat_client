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
            /*Obtains server information from the user. */
            Client.Entry UserEntry = new Client.Entry();
            Client.Entry.ClientRequestInfo settings = UserEntry.FindServer();

            /*load settings and starts the server*/
            Client.Client client = new Client.Client(settings);
            client.Start();

  
        }
    }
}
