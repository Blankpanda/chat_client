using System.Collections.Generic;

namespace Chat_Client.Tools
{
    internal class IO
    {
        /// <summary>
        /// write a list to a text file.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        internal static void WriteToTextFile(List<string> list, string path)
        {
            using (System.IO.StreamWriter w = new System.IO.StreamWriter(path))
            {
                foreach (string elem in list)
                {
                    w.WriteLine(elem);
                }
            }
        }

        /// <summary>
        /// read a text file and conver its line to a list
        /// </summary>
        /// <param name="ConfigFilePath"></param>
        /// <returns></returns>
        internal static List<string> ReadTextFileList(string path)
        {
            List<string> contents = new List<string>();

            using (System.IO.StreamReader r = new System.IO.StreamReader(path))
            {
                string file = r.ReadToEnd(); // read the file
                string[] lines = file.Split('\n'); // split it up by new lines

                for (int i = 0; i < lines.Length; i++) // remove \r return character
                    lines[i] = lines[i].Replace("\r", "");

                for (int i = 0; i < lines.Length; i++) // convert into a list
                    contents.Add(lines[i]);
            }

            return contents;
        }
    }
}