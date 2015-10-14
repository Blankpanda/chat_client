using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure
{
    class CommandList
    {
        enum Commands
        {
            CLEAR = 1,
            CREATE   ,
            DELETE   ,
            EXIT     ,
            HELP     ,           
            PING     ,
            SLIST    ,
            START    ,
        }
         int NUMBER_OF_COMMANDS = Enum.GetValues(typeof(Commands)).Length;




        // create a list of commands               
        public  List<string> GetCommands()
        {
            List<string> commands = new List<string>();
            for (int i = 0; i < NUMBER_OF_COMMANDS; i++)
                commands.Add(Enum.GetName(typeof(Commands), i));
            return commands;
            
        }

    }
}
