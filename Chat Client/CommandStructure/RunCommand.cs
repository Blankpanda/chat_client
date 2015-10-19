using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure
{
    
    class RunCommand
    {
        public void Prompt(string prompt)
        {
            Console.Write(prompt);
        }
       
        
        public void Run(string command)
        {
            // setup loggers.
            Server.Logger CommandHistoryLog = new Server.Logger(Server.LogType.Type.HISTORY);
          
            command = command.Trim();
            command = command.ToUpper();

            // generates a list of commands based off of CommandList.Commands Enum
            CommandList CList = new CommandList();
            List<string> CommandList = CList.GetCommands();
            
            /* error checking */
               
            // Invalid Command
            int err = 0; 
            for (int i = 0; i < CommandList.Count; i++)
                if (command != CommandList[i]) 
                    err++;
                    
                
            // The command doesn't exist.
            if (err == CommandList.Count && command != "")
                Console.WriteLine("Invalid Command.  type 'Help' for a list of commands.");


            // Execute a correct command
            switch (command)
            {
                case "CLEAR":
                    CommandHistoryLog.Write("Clear command issued to console.");

                    Commands.Clear.Execute();                    
                    break;

                case "CREATE":
                    CommandHistoryLog.Write("Create command issued to console.");

                    Commands.Create.Execute();                    
                    break;

                case "DELETE":
                    CommandHistoryLog.Write("Delete command issued to console.");

                    Commands.Delete.Execute();
                    break;

                case "Exit":
                    CommandHistoryLog.Write("Exit command issued to console.");

                    Commands.Delete.Execute();
                    break;

                case "HELP":
                    CommandHistoryLog.Write("Help command issued to console.");

                    Commands.Help.Execute();
                    break;

                case "?":
                    CommandHistoryLog.Write("Help command issued to console.");

                    Commands.Help.Execute();
                    break;
                    
                case "PING":
                    CommandHistoryLog.Write("Ping command issued to console.");

                    Commands.Ping.Execute();
                    break;

                case "SLIST":
                    CommandHistoryLog.Write("SList command issued to console.");

                    Commands.SList.Execute();
                    break;

                case "START":
                    CommandHistoryLog.Write("SList command issued to console.");

                    Commands.Start.Execute();
                    break;

            }    
                
            
      
        }


    }
}
