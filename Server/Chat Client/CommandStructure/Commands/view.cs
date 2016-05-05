namespace Chat_Client.CommandStructure.Commands
{
    internal class View : ICommand
    {
        private string _Name = "view";
        private string _Desc = "View server settings from a select server";

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
            Server.ServerList slist = new Server.ServerList();
            //string[] ServerList = slist.GetServerList(); // gets a list of all of the created servers from the Servers/ directory.

            slist.PrintServerSettings();
        }
    }
}