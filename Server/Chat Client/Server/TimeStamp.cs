using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_Client.Server
{
    class TimeStamp
    {
        // appends a time stamp to an inputed string
        public static string WriteTime(string s)
        {

            string stamp = "[" + DateTime.Now.ToString() + "]";            
            return stamp + s;
        }

        public static string GetTime()
        {
            string stamp = "[" + DateTime.Now.ToString() + "]";
            return stamp;
        }
    }
}
