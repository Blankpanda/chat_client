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
            CLS      ,
            CREATE   ,
            DELETE   ,
            RM       ,
            EXIT     ,
            HELP     ,                     
            PING     ,
            SLIST    ,
            LS       ,
            START    ,
            ALIAS    ,
            VIEW     ,
        }
         int NUMBER_OF_COMMANDS = Enum.GetValues(typeof(Commands)).Length;




        // create a list of commands               
        public  List<string> GetCommands()
         {
             List<string> commands = new List<string>();
             for (int i = 0; i <= NUMBER_OF_COMMANDS; i++)
                commands.Add(Enum.GetName(typeof(Commands), i));
             commands.Add("?"); // we can't use the `?` character as a enum so need to hard add it to the list.
             return commands;

         }

    }
}
