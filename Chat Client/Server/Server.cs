using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client.Server
{
    /// <summary>
    /// Listens for connections and makes desicions based on that.
    /// </summary>
    class Server
    {
       
        private ServerInit.ServerSettings settings;

        public Server(ServerInit.ServerSettings ServerConfiguration)
        {
            settings = ServerConfiguration;
        }
        

        public void Listen()
        {

        }

    }
}
