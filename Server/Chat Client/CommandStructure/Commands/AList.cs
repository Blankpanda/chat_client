using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
    /// <summary>
    /// Generate a list of Aliases.
    /// </summary>
    class AList : ICommand
    {
        private string _Name = "AList";
        private string _Desc = "Generates a list of aliases";
        
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

        public string Alias
        {
            get { return null; }
            set { Alias = null; }
        }

        public void Execute()
        {
            List<string> aliases = new List<string>();
            aliases.Add("clear - cls");
            aliases.Add("slist - ls");
            aliases.Add("help - ?");
            aliases.Add("delete - rm");

            foreach (string alias in aliases)
                Console.WriteLine("   " + alias);                            
        }

    }
}
