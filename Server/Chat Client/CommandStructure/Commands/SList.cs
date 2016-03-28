using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    /// supplies a list of the current servers.
    /// </summary>
    class SList
    {

        private string _Name = "slist";
        private string _Desc = "Generates a List of created servers.";
        private string _Alias = "ls";
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

        public string Alias
        {
            get { return _Alias;  }
            set { Alias = _Alias; }
        }

        public static void Execute()
        {
            Server.ServerList srvList = new Server.ServerList();

            srvList.DisplayServerList();
        }
    }
}
