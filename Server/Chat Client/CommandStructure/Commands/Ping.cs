namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  a simple built in diagnostics tool so the user can verify connectivity.
    /// </summary>
    internal class Ping : ICommand
    {
        private string _Name = "ping";
        private string _Desc = "Sends a simple ICMP to a target IP.";

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
            Tools.Net pinger = new Tools.Net();

            pinger.PingAddress();
        }

        public static void Execute(string addr)
        {
            Tools.Net pinger = new Tools.Net();

            pinger.PingAddress(addr);
        }
    }
}