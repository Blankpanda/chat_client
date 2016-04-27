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
                      
            command = command.Trim().ToUpper();           

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
                    {
                        Commands.Clear.Execute();
                    } break;
                                        
                case "CLS":
                    {
                        Commands.Clear.Execute();
                    } break;                                        

                case "CREATE":
                    {
                        Commands.Create.Execute();
                    } break;                      
                  
                case "DELETE":
                    {
                        Commands.Delete.Execute();
                    } break;

                case "RM":
                    {                        
                        Commands.Delete.Execute();
                    } break;                    

                case "Exit":
                    {
                        Commands.Exit.Execute();
                    } break;

                case "HELP":
                    {
                        Commands.Help.Execute();
                    } break;

                case "?":
                    {
                        Commands.Help.Execute();
                    } break;
                                        
                case "PING":
                    {
                        Commands.Ping.Execute();
                    } break;

                case "SLIST":
                    {
                        Commands.SList.Execute();
                    } break;

                case "LS":
                    {
                        Commands.SList.Execute();
                    } break;

                case "START":
                    {
                        Commands.Start.Execute();
                    } break;

                case "ALIAS":
                    {
                        Commands.Alias.Execute();
                    } break;
                case "VIEW":
                    {
                        Commands.View.Execute();
                    } break;
            }    
        }

        /*Overload to contain arguments*/
        public void Run(string[] args)
        {       
            string command = args[0];  // the first element is the command.
            string argument = args[1]; // the second element is the argument to the command.

            command = command.Trim().ToUpper();            
            argument = argument.Trim().ToUpper();
            

            // generates a list of commands based off of CommandList.Commands Enum
            CommandList CList = new CommandList();
            List<string> CommandList = CList.GetCommands();

            /* error checking */

            // Too Many Arguments.
            if (args.Length > 2)
                Console.WriteLine("Invlaid number of Arugments supplied for " + command + ".");
                
            

            // get a number that specifies whether or not the command uses exists.
            // if err is equal to ComandList.Count, the command doesn't exist.
            int err = 0;
            for (int i = 0; i < CommandList.Count; i++)
                if (command != CommandList[i])
                    err++;


            // The command doesn't exist.
            if (err == CommandList.Count && command != "")
                Console.WriteLine("Invalid Command.  type 'Help' for a list of commands.");


            // The List of all of the commands without arguments
            List<string> NoArgsCommands = CList.GetCommands();
            NoArgsCommands.Remove("DELETE");
            NoArgsCommands.Remove("PING");
            NoArgsCommands.Remove("START");


            // Execute a correct command
            switch (command)
            {
                case "DELETE":
                    {
                        Commands.Delete.Execute(argument);

                    } break;
                case "RM":
                    {
                        Commands.Delete.Execute(argument);
                    } break;
                case "PING":
                    {
                        Commands.Ping.Execute(argument);
                    } break;
                case "START":
                    {
                        Commands.Start.Execute(argument);
                    } break;                
                default:
                    {
                        for (int i = 0; i < NoArgsCommands.Count; i++)
                            if (command == NoArgsCommands[i])
                                Console.WriteLine("The " + command + " command doesn't allow for arguments.");                                         
                    } break;

            }
        }
    }
}
