﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
namespace Client
{
    /* THIS CLASS IS A COPY OF A CLASS IN THE SERVER PROJECT. 
        with some modifications. */
    class Net
    {
        private int scount = 0;

        public int SuccessCount
        {
            get { return scount; }
            set { scount = SuccessCount; }
        }
        
        
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


        /* Ping an entered address */
        public void PingAddress(string addr)
        {
            List<IPStatus> replies = new List<IPStatus>();

            int count = 0;

            // send 4 pings
            while (count < 4)
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

                replies.Add(Reply.Status);

                ++count;
            }


            // tracks the ammount of successful replies
            for (int i = 0; i < replies.Count; i++)            
                if (replies[i] == IPStatus.Success)                
                    scount++;                           
        }
    }
}
