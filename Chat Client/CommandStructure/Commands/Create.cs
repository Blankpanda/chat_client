using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{

    /// <summary>
    /// This command is used to create a new instance of a server.
    /// this class class the ServerInit class and runs the creator
    /// the user must supply the following arguments - 
    ///     server_name 
    ///     backlog 
    ///     server_ip_address  
    ///     port_number
    ///     password
    /// </summary>
    
    class Create
    {

        private string _Name = "Create";
        private string _Desc = "allows the user to create a new server by inputing a " +
                               "\n\tname\n\tbacklog\n\tIP\n\tPort\n\tPassword";
        public string Name
        {
            get { return _Name; }
            set { Name = _Name; }
        }

        public string Description
        {
            get { return _Desc; }
            set { Description = _Desc; }
        }

        public static void Execute()
        {
            ServerInit InitalizeServer = new ServerInit();

            InitalizeServer.Create();
        }
        
    }
}
