using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{

    /// <summary>
    /// 
    /// </summary>
    class Create
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
            ServerInit InitalizeServer = new ServerInit();

            InitalizeServer.Create();
        }
        
    }
}
