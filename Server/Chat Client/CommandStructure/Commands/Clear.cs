using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    ///  This command is used to clear the console window.
    ///  using console.Clear();
    /// </summary>  
    
    class Clear
    {
        private string _Name = "Clear";
        private string _Desc = "Clears the contents of the console.";
        public string Name
        {
            get { return _Name; }
            set { Name = _Name; }
        }

        public string Description
        {
            get { return _Desc; }
            set { Description = _Desc; }
        }

        public static void Execute()
        {
            Console.Clear();
        }
    }
}
