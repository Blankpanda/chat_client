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

        private string _Name;
        private string _Desc;
        public string Name
        {
            get { return _Name; }
            set { Description = _Name; }
        }

        public string Description
        {
            get { return _Desc; }
            set { Description = _Desc; }
        }

        public static void Execute()
        {

        }
    }
}
