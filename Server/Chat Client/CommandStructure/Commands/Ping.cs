using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  a simple built in diagnostics tool so the user can verify connectivity.
    /// </summary>
    class Ping
    {

        private string _Name = "Ping";
        private string _Desc = "Sends a simple ICMP to a target IP.";
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

            Net pinger = new Net();

            pinger.PingAddress();

        }
        public static void Execute(string addr)
        {

            Net pinger = new Net();

            pinger.PingAddress(addr);

        }
    }
}
