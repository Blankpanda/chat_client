using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client
{
    /// <summary>
    /// Listens for connections and makes desicions based on that.
    /// </summary>
    class Server
    {
       
        private ServerInit.ServerSettings settings;

        public Server(ServerInit.ServerSettings ServerConfiguration)
        {
            settings = ServerConfiguration; // the configuration of the server that is being initalzied
        }
        

        // this is used to organzie RunCommand.cs
        public void Start()
        {
            CommandStructure.RunCommand command = new CommandStructure.RunCommand(); // change the prompt.
            command.Prompt(settings.server_name + ">");


            Listen(settings);
            
        }

        private void Listen(ServerInit.ServerSettings settings)
        {

        }

    }
}
