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
    class ServerInit 
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
        private static ServerSettings Init(string ServerName)
        {
            ServerList SL = new ServerList();
            string[] ServerList = SL.GetServerList(); // list of all directory paths
            string ServerPath = "";
            // ex: ServerList[0] = Server\Caleb
            
            
            

            // we want to remove the parent directory from the path so we can retrieve the name
            // for checking
            // then we populate ConfigurationFile with elements of the txt file.
            // we then use a SeverSettings to pass into Listen()
           
            for (int i = 0; i < ServerList.Length; i++)            
                ServerList[i] = ServerList[i].Remove(0, SL.MainServerDirectory.Length + 1);


            for (int i = 0; i < ServerList.Length; i++)
            {
                if ( ServerName == ServerList[i])
                {
                    ServerPath = SL.MainServerDirectory + @"\" + ServerName;
                }
            }

            //gets the configuration file from the Servers\myserver directory
            DirectoryInfo ConfigFileInfo = new DirectoryInfo(ServerPath);
            FileInfo[] files = ConfigFileInfo.GetFiles();
            string ConfigFilePath = "";
            
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".txt" && files[i].Name.Contains("_Config"))
                {
                    ConfigFilePath = files[i].FullName;
                }
   
            }

            // used when reading config file
            List<string> ConfigurationFile = SL.ReadTextFileList(ConfigFilePath);
            ServerSettings settings = new ServerSettings();

            settings.server_name        = ConfigurationFile[1];
            settings.backlog            =  int.Parse( ConfigurationFile[2] );
            settings.server_ip_address  = ConfigurationFile[3];
            settings.port_number        =  int.Parse ( ConfigurationFile[4] );

            Console.WriteLine(settings.server_name + settings.backlog + settings.server_ip_address + settings.port_number);

            return settings;
        }       
    }
}
