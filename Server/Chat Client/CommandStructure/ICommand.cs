using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure
{
    interface ICommand
    {
        string Name
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }

        string Alias
        {
            get;            
        }

        void Execute();        
    }
}
