namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  This command is used to remove a severs configuration file from the Servers/ directory
    ///  The Delete function is contain in ServerList.cs.
    /// </summary>

    internal class Delete
    {
        private string _Name = "delete";
        private string _Desc = "Removes a Server from the /Server Directory";
        private string _Alias = "rm";

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
            Server.ServerList Slist = new Server.ServerList();

            Slist.Delete();
        }

        public static void Execute(string ServerName)
        {
            Server.ServerList Slist = new Server.ServerList();

            Slist.Delete(ServerName);
        }
    }
}