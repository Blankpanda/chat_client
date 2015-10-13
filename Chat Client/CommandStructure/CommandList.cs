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
            Create = 1,
            Delete = 2,
            Exit = 3,
            Ping = 4,
            SList = 5,
            Start = 6,
        }
        const int NUMBER_OF_COMMANDS = 6;




        // create a list of commands               
        public  List<string> GetCommandList()
        {
            List<string> commands = new List<string>();
            for (int i = 0; i < NUMBER_OF_COMMANDS; i++)
                commands.Add(Enum.GetName(typeof(Commands), i));
            return commands;
            
        }

    }
}
