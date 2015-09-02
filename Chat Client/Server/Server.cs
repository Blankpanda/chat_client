using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client
{
    class Server
    {
        
        private static string data = null;
        

        /* settings of the server */
        public struct ServerSettings
        {
            public int port_number, backlog;
            public string server_ip_address ,server_name;

            public ServerSettings(int port, int log, string ip, string name)
            {
                port_number = port;
                backlog = log;
                server_ip_address = ip;
                server_name = name;
            }

        }

        /* used to a new server entry */
        public static void CreateNewServer()
        {
            ServerSettings settings = new ServerSettings();

            // settings.server_name
            Console.WriteLine("Enter the name of the new server: ");
            settings.server_name = Console.ReadLine();

            // settings.backlog
            Console.WriteLine("Enter the backlog for {0}: ", settings.server_name);
            settings.backlog = int.Parse( Console.ReadLine() );

            // settings.port_number
            Console.WriteLine("Enter the port number for {0} ", settings.server_name);
            settings.port_number = int.Parse( Console.ReadLine() );

            // settings_ip_address
            IPHostEntry host = Dns.Resolve(Dns.GetHostName() ); // only need the IP as a string here

            // iterate through the hosts address list to find the private address
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (IsPrivateAddress(host.AddressList[i].ToString()))
                {
                    settings.server_ip_address = host.AddressList[i].ToString();
                }    
            }


            Create(settings);
        }
        /* checks to see if an address is private and returns true if it is. */
        private static bool IsPrivateAddress(string address)
        {
            return false;

            // class A

            // class B

            // class C

        }
        private static void Create(ServerSettings settings)
        {

        }
        
        /* this method is used when the server starts to search and load settings to be passed to the Listen() method */
        private static void Init(string ServerName)
        {

        }

        /* this method is used to listen for incoming connections */
        public static void Listen(ServerSettings ServerSettings)
        {

        }

        
    }
}
