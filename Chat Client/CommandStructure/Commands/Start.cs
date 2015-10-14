using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    /// starts a supplied server from name.
    /// </summary>
    class Start
    {

        private string _Name = "Start";
        private string _Desc = "Starts a server by name";
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

        }
    }
}
