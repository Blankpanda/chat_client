﻿using System;
using System.Collections.Generic;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    /// Generate a list of Aliases.
    /// </summary>
    internal class Alias : ICommand
    {
        private string _Name = "alias";
        private string _Desc = "Generates a list of aliases";

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

        public static void Execute()
        {
            List<string> aliases = new List<string>();
            aliases.Add("clear - cls");
            aliases.Add("slist - ls");
            aliases.Add("help - ?");
            aliases.Add("delete - rm");

            foreach (string alias in aliases)
                Console.WriteLine("   " + alias);
        }
    }
}