namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  Terminates the program and shutsdown any active server.
    /// </summary>

    internal class Exit : ICommand
    {
        private string _Name = "exit";
        private string _Desc = "Closes out of the console window.";

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
        }
    }
}