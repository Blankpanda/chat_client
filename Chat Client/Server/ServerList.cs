using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Chat_Client
{
    /// <summary>
    /// Used to maintain and add to a list of directories 
    /// </summary>
    class ServerList
    {
        private string ServerDirectory = "Servers";

        /* Initalizes a directory in the constructor to hold all fo the servers config files */        
        public ServerList()
        {
            Directory.CreateDirectory(ServerDirectory);
        }

        /* Creates a directory to store the configuration file and writes the configuration file */
        internal void Add(Server.ServerSettings settings)
        {
            string NewDirectory = ServerDirectory + @"\" + settings.server_name; // EX Servers\myserver\
            Directory.CreateDirectory(NewDirectory);

            List<string> settings_content = new List<string>();
            // build a list that makes the configuration file

            // Example config file:
            //      server #
            //      name 
            //      backlog
            //      IP
            //      Port

            int ServerNumber = GetServerListCount();


            settings_content.Add(ServerNumber.ToString());  // Server #
            settings_content.Add(settings.server_name);     // Name
            settings_content.Add(settings.backlog.ToString());  // Backlog
            settings_content.Add(settings.server_ip_address);   // IP
            settings_content.Add(settings.port_number.ToString());  //  Port

            WriteToTextFile(settings_content, ServerDirectory);

        }

        /* Writes to a Text File for the servers configuration. */
        // TODO: add execption code if the server already exists.

        private void WriteToTextFile(List<string> content, string path)
        {
            path = path  + @"\" + content[1] + @"\" + content[1] +  "_Config" + ".txt" ; // content[1] = ServerSettings.Names
            using ( StreamWriter writer = new StreamWriter(path) )
            {
                for (int i = 0; i < content.Count; i++)
                {
                    writer.WriteLine(content[i]);
                }
            }
        }
        /* Reads a text file  and returns the output in a list or a string.*/
        private string ReadTextFile(string FileName)
        {

            return "";
        }


        /* Gets the total number of directories in the servers directory */
        // TODO: this is really hacky so I would like to redo this at some point
        public int GetServerListCount()
        {
            int ServerCount = Directory.GetDirectories(ServerDirectory, "*").Length;
            return ServerCount;
        }
    }
}
