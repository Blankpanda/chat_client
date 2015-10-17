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
            command = command.Trim();
            command = command.ToUpper();
            // generates a list of commands based off of CommandList.Commands Enum
            CommandList CList = new CommandList();
            List<string> CommandList = CList.GetCommands();
            
            /* error checking */


             
            //if (string.IsNullOrEmpty(command))            
            //    Console.WriteLine("Missing Input. type 'Help' for a list of commands.");


            // Invalid Command

            int err = 0; 
            for (int i = 0; i < CommandList.Count; i++)
                if (command != CommandList[i]) 
                    err++;
                    
                

            if (err == CommandList.Count && command != "") // we already cover string.Empty above
                Console.WriteLine("Invalid Command.  type 'Help' for a list of commands.");


            // Execute a correct command
            switch (command)
            {
                case "CLEAR":
                    Commands.Clear.Execute();
                    break;
                case "CREATE":
                    Commands.Create.Execute();
                    break;
                case "DELETE":
                    Commands.Delete.Execute();
                    break;
                case "Exit":
                    Commands.Delete.Execute();
                    break;
                case "HELP":
                    Commands.Help.Execute();
                    break;
                case "?":
                    Commands.Help.Execute();
                    break;
                case "PING":
                    Commands.Ping.Execute();
                    break;
                case "SLIST":
                    Commands.SList.Execute();
                    break;
                case "START":
                    Commands.Start.Execute();
                    break;              
            }    
                
            
      
        }


    }
}
