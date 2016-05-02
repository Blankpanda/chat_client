using System;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  This command is used to clear the console window.
    ///  using console.Clear();
    /// </summary>

    internal class Clear
    {
        private string _Name = "clear";
        private string _Desc = "Clears the contents of the console.";
        private string _Alias = "cls";

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

        public static void Execute()
        {
            Console.Clear();
        }
    }
}