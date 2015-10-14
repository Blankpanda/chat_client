﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  This command is used to remove a severs configuration file from the Servers/ directory
    ///  The Delete function is contain in ServerList.cs.
    /// </summary>
    
    class Delete
    {

        private string _Name = "Delete";
        private string _Desc = "Removes a Server from the /Server Directory";
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
            ServerList Slist = new ServerList();


            Slist.Delete();

        }
    }
}
