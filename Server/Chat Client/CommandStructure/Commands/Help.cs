﻿using System;
using System.Collections.Generic;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    /// generates a list of commands and a description on how they are used.
    /// </summary>
    ///

    internal class Help : ICommand
    {
        private string _Name = "help";
        private string _Desc = "Outputs this display";
        private string _Alias = "?";

        public string Name
        {
            get { return _Name; }
            set { Name = _Name; }
        }

        public string Description
        {
            get { return _Desc; }
            set { Description = _Desc; }
        }

        public string Alias
        {
            get { return _Alias; }
            set { Alias = _Alias; }
        }

        // This is used to contain all of the props name and description props in each command
        // initalized in the constructor
        private static List<string> HelpDisplay =
                new List<string>();

        public static void Execute()
        {
            // horrible hack

            Clear clear = new Clear();
            Create create = new Create();
            Delete delete = new Delete();
            Exit exit = new Exit();
            Ping ping = new Ping();
            SList slist = new SList();
            Start start = new Start();
            Help help = new Help();
            Alias alias = new Alias();
            View view = new View();

            // im sorry mom
            HelpDisplay.Add("   " + clear.Name + " - " + clear.Description);
            HelpDisplay.Add("   " + create.Name + " - " + create.Description);
            HelpDisplay.Add("   " + delete.Name + " - " + delete.Description);
            HelpDisplay.Add("   " + exit.Name + " - " + exit.Description);
            HelpDisplay.Add("   " + ping.Name + " - " + ping.Description);
            HelpDisplay.Add("   " + slist.Name + " - " + slist.Description);
            HelpDisplay.Add("   " + start.Name + " - " + start.Description);
            HelpDisplay.Add("   " + alias.Name + " - " + alias.Description);
            HelpDisplay.Add("   " + help.Name + " - " + help.Description);
            HelpDisplay.Add("   " + view.Name + " - " + view.Description);

            foreach (string command in HelpDisplay)
                Console.WriteLine(command);

            HelpDisplay.Clear(); // static list.
        }
    }
}