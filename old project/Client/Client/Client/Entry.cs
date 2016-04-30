using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client.Client
{
    class Entry
    {
        /* Information gained from the user will be stored here and used in Client.cs 
         * to be used when connecting to specifed server */
        
        public struct ClientRequestInfo
        {
            public string ip_address, password, username;
            public int    port_number;            

            ClientRequestInfo(string addr, string pass, int port, string uname)
            {
                ip_address  = addr;
                password    = pass;
                username    =uname;
                port_number = port;
                
            }      

        }

        public ClientRequestInfo FindServer()
        {
            ClientRequestInfo settings = new ClientRequestInfo();

            Console.WriteLine("Press 'Enter' to find a server.");
            Console.ReadLine();
            Console.Clear();
            settings.ip_address = GetServerIP();
            settings.port_number = GetServerPort();
            settings.username = GetUserName();

            Console.WriteLine("Press Enter' to continue.");
            
            return settings;
        }

        private string GetUserName()
        {
            string inName = "";
            while(true)
            {
                Console.WriteLine("Please Enter in your display name:");
                try
                {
                    inName = Console.ReadLine();
                    Console.Clear();
                    return inName;
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid name entered.");
                    continue;
                }
                
                
                
            }
        }

        /* Asks the user for the server that they want connect to.*/
        private string GetServerIP()
        {
            string inAddr = "";
            while (true)
            {
                Console.WriteLine("Enter in the IP address of the server: ");
                inAddr = Console.ReadLine();
                inAddr = inAddr.Trim();

                if (Net.IsPrivateAddress(inAddr))
                {
                    if (Net.CheckAddress(inAddr))
                    {
                        break;
                    }
                }                    
                else
                {
                    Console.WriteLine("Invalid entry. format: X.X.X.X");
                    continue;
                }

            }
            Console.Clear();
            return inAddr;
        }

                         
        /* Gets the desired port from the user. 7777 by default. */
        private int GetServerPort()
        {
            string inPort = "";
            int numberPort = 0;                    

            while (true)
            {
                Console.WriteLine("Enter in the port number for the server. hitting 'Enter' will default to 7777.");
                try
                {
                    inPort = Console.ReadLine();
                    inPort = inPort.Trim();

                    if (inPort == "")
                    {
                        Console.Clear();
                        return 7777;                        
                    }
                    else
                    {
                        numberPort = int.Parse(inPort);
                        Console.Clear();
                        return numberPort;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input entered.  use a number.");
                }
              
            }
        }


        ///*If the server is found on the network, get entry for the password the user gave. */
        //private string GetPassword()
        //{

        //}



        internal void CheckServer(ClientRequestInfo settings)
        {
            int err = 0;

            // Check the IP address.
            Console.WriteLine("Checking" + settings.ip_address + "Server address...");            
            if(Net.CheckAddress(settings.ip_address))
            {
                Console.WriteLine("Address Found.");
            }
            else
            {
                err++;
                Console.WriteLine("Failed to find address.");
            }


            // Check if the port is open
            Console.WriteLine("Checking to see if port " + settings.port_number.ToString() + " is open...");
            using(System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient())
            {
                try
                {
                    tcp.Connect(settings.ip_address, settings.port_number);
                    Console.WriteLine(settings.port_number.ToString() + " is open.");
                }
                catch (Exception)
                {
                    err++;
                    Console.WriteLine(settings.port_number.ToString() + " is not open");                    
                }
            }

            // TODO: Check if the password is correct.            

            if (err == 0)
            {
                Console.WriteLine("Starting Connection to Server...");
            }
            else
            {
                // TODO: later we can just have the application close out
                Console.WriteLine("Connection to Server Failed. Shuting down.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
