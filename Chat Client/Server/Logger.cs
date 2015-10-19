using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chat_Client.Server
{
    class Logger
    {

        /*We want the logger that is being defined to write to the same file.  we also want to use the logger class for several different types of logs. */

        private string LogName = "";

        public Logger(LogType.Type Type)
        {
            switch (Type)
            {
                case LogType.Type.CHAT:     // Chat log.
                    LogName = "chat_log.text";
                    break;
                case LogType.Type.EVENT:    // Event history.
                    LogName = "event_log.text";
                    break;
                case LogType.Type.HISTORY:  // command history.
                    LogName = "history_log.text";
                    break;
                case LogType.Type.READER:   // were not doing anything special here. we just need to use this class for reading a text file.
                    break;
                default:
                    break;
            }
        }

        public void Write(string s)
        {
            
        }

        
        /* Writes to a Text File for the servers configuration. */

        public static void WriteToTextFile(List<string> content, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < content.Count; i++)
                {
                    writer.WriteLine(content[i]);
                }
            }
        }

        /* Reads a text file  and returns the output in a list or a string.*/

        //  for List
        public List<string> ReadTextFileList(string FileName)
        {
            List<string> elems = new List<string>();
            using (StreamReader reader = new StreamReader(FileName))
            {
                if (File.Exists(FileName))
                {
                    while (reader.Peek() >= 0)
                    {

                        elems.Add(reader.ReadLine());

                    }
                }

            }

            return elems;
        }

        //  for string (string is appended with new line constant).
        public string ReadTextFileString(string FileName)
        {
            StringBuilder sb = new StringBuilder();

            using (StreamReader reader = new StreamReader(FileName))
            {
                if (File.Exists(FileName))
                {
                    while (reader.Peek() >= 0)
                    {

                        sb.Append(reader.ReadLine() + "\n");

                    }


                }
            }

            string readFile = sb.ToString();

            return readFile;
        }

    }
}
