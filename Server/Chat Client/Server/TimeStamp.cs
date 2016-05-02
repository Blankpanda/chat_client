using System;

namespace Chat_Client.Server
{
    internal class TimeStamp
    {
        // appends a time stamp to an inputed string
        public static string WriteTime(string s)
        {
            string stamp = "[" + DateTime.Now.ToString() + "]";
            return stamp + " " + s;
        }

        public static string GetTime()
        {
            string stamp = "[" + DateTime.Now.ToString() + "]";
            return stamp;
        }

        internal static string WriteTimeNoDate(string data)
        {
            string stamp = "[" + DateTime.Now.ToLocalTime() + "]";
            return stamp + " " + data;
        }
    }
}