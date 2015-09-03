using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
        public void CreateNewServer()
        {
            ServerSettings settings = new ServerSettings();
            ServerList serverlist = new ServerList();

            // data validation
            string  inStr = ""; 
            int     inInt = 0;


            // settings.server_name
            Console.WriteLine("Enter the name of the new server: ");
            while (true)
            {
                inStr = Console.ReadLine();

                if (!(string.IsNullOrEmpty(inStr)))
                {
                    if (!(Directory.Exists(serverlist.MainServerDirectory + @"\" + inStr)))
                    {
                        settings.server_name = inStr;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The server already exists");
                    }
                   
                }
                else
                {
                    Console.WriteLine("please enter in a name for the server");
                }                                  
                inStr = "";
            }
            

            // settings.backlog       
            while(true)
            {
                Console.WriteLine("Enter the backlog for {0}: ", settings.server_name);
                inInt = 0;
                try
                {
                    inInt = int.Parse( Console.ReadLine());
                    
                    settings.backlog = inInt;
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input, please enter in a number \n(hint): try 5");                    
                }
                

            }         

            // settings.port_number
            
            while (true)
            {
                Console.WriteLine("Enter the port number for {0} ", settings.server_name);
                inInt = 0;
                try
                {
                    inInt = int.Parse(Console.ReadLine());

                    settings.port_number = inInt;
                    break;
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Input, please enter in a valid port number \n(hint: try 12000");
                }
                
            }
            

            // settings_ip_address
            IPHostEntry host = Dns.Resolve(Dns.GetHostName() ); // only need the IP as a string here

            // TODO: iterate through the hosts address list to find the private address
           
            settings.server_ip_address = host.AddressList[1].ToString();

            Create(settings);
        }

        
        // TODO: this needs to get done but for now we can use host.AddressList[1].
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
            ServerList Servers = new ServerList();
            Servers.Add(settings);
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
