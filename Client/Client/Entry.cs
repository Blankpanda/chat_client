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
            public string ip_address, password;
            public int    port_number;

            ClientRequestInfo(string addr, string pass, int port)
            {
                ip_address  = addr;
                password    = pass;
                port_number = port;
            }      

        }

        public ClientRequestInfo FindServer()
        {
            ClientRequestInfo settings = new ClientRequestInfo();

            Console.WriteLine("Press 'Enter' to find a server.");
            Console.ReadLine();

            settings.ip_address = GetServerIP();
            settings.port_number = GetServerPort();
            
            return settings;
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
                    if (CheckAddress(inAddr))
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
                        return 7777;                        
                    }
                    else
                    {
                        numberPort = int.Parse(inPort);
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

        /*Pings the entered address to see if the address is on the network.*/
        private bool CheckAddress(string addr)
        {
            Console.WriteLine("Checking address...");
            Net Pinger = new Net();

            Pinger.PingAddress(addr); // ping the supplied address to track replies.

            if (Pinger.SuccessCount >= 1)
            {
                Console.WriteLine("Address found.");
                return true;
            }
            else            
                Console.WriteLine("Address was not found.");              
            

                return false;
        }
    }
}
