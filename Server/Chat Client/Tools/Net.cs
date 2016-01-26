using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Chat_Client.Tools
{
    class Net
    {
        public static bool IsPrivateAddress(string address)
        {
            string[] octs = address.Split('.'); // checks to see if the vlaues are in the right format
            if (octs.Length != 4)
            {
                return false;
            }

            byte b = 0; // checks each value in octs to make sure its a valid byte
            for (int i = 0; i < octs.Length; i++)
            {
                if (!byte.TryParse(octs[i], out b))
                {
                    return false;
                }

            }

            return true;
        }
        
        public void PingAddress()
        {
            Console.WriteLine("Enter in an address to ping.");
            string addr = Console.ReadLine();

            
            int count = 0;

            if (IsPrivateAddress(addr))
            {
                while (count <= 4)
                {

                    // used to construct a 32 byte message 
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;

                    // intalize the ping object with ttl and no fragment options
                    PingOptions PingSettings = new PingOptions(53, true);
                    Ping Pinger = new Ping();

                    // send the ping
                    PingReply Reply = Pinger.Send(addr, timeout, buffer, PingSettings);

                    // output the statistics of the reply to the console window.

                    Console.Write("Ping #1: Reply from "
                                + Reply.Address + " size:"
                                + Reply.Buffer.Length.ToString() + " time:" +
                                Reply.RoundtripTime.ToString() + " status:" +
                                Reply.Status.ToString());
                    Console.WriteLine();

                    ++count;
                }
            }
            else
            {
                Console.WriteLine("The entered address is not an IP address.");
            }
            
        }


        /* Overload to have an already supplied address ping.*/
        public void PingAddress(string addr)
        {
            int count = 0;

            if (IsPrivateAddress(addr))
            {
                while (count <= 4)
                {

                    // used to construct a 32 byte message 
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;

                    // intalize the ping object with ttl and no fragment options
                    PingOptions PingSettings = new PingOptions(53, true);
                    Ping Pinger = new Ping();

                    // send the ping
                    PingReply Reply = Pinger.Send(addr, timeout, buffer, PingSettings);

                    // output the statistics of the reply to the console window.

                    Console.Write("Ping #1: Reply from "
                                   + Reply.Address + " size:"
                                   + Reply.Buffer.Length.ToString() + " time:" +
                                    Reply.RoundtripTime.ToString() + " status:" +
                                    Reply.Status.ToString());
                    Console.WriteLine();

                    ++count;
                }
            }
            else
            {
                Console.WriteLine("The entered address is not an IP address.");
            }

        }
    }
}
