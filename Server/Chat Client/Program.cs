using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chat Server application by Caleb Ellis (2015).");
            Console.WriteLine();

            Server.Logger EventLogger = new Server.Logger(Server.LogType.Type.EVENT);
            EventLogger.Write("Program Started.");

            // handles command input
            CommandStructure.RunCommand CommandRun = new CommandStructure.RunCommand();

            // initalizes the prompt
            CommandRun.Prompt(">");

            // REEPL
            while (true)
            {              
                string request =
                 Console.ReadLine();
                request = request.Trim();

                string[] arguments = request.Split(' ');

                if (request.ToUpper() == "EXIT")
                {
                    EventLogger.Write("Program Exited.");
                    break;
                }
                else
                {
                    if (arguments.Length > 1 )                    
                        CommandRun.Run(arguments);                    
                    else                                        
                        CommandRun.Run(request);


                    CommandRun.Prompt(">"); // restore the prompt

                }

            }


        }
    }
}
