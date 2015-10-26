using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Client
{
   
    class Chat
    {
       
        public string GetMessageFromStream()
        {
            Console.Write(":");
            string input = Console.ReadLine();
            return input;
        }
    }
}
