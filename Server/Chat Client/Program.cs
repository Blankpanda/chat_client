﻿using System;

namespace Chat_Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Server Management Application by Caleb Ellis (2015).");
            Console.WriteLine();

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
                    break;
                }
                else
                {
                    if (arguments.Length > 1)
                        CommandRun.Run(arguments);
                    else
                        CommandRun.Run(request);

                    CommandRun.Prompt(">"); // restore the prompt
                }
            }
        }
    }
}