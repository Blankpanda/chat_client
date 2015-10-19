﻿using System;
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

            Server.TimeStamp.WriteTime();
            // handles command input
            CommandStructure.RunCommand CommandRun = new CommandStructure.RunCommand();

            // initalizes the prompt
            CommandRun.Prompt(">");

            while (true)
            {              
                string request =
                 Console.ReadLine();

                if (request.ToUpper() == "EXIT")
                {
                    break;
                }
                else
                {
                    CommandRun.Run(request);
                    CommandRun.Prompt(">"); // restore the prompt
                }

            }


        }
    }
}
