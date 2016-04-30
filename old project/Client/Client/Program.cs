using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static List<string> area1 = new List<string>();
        static List<string> area2 = new List<string>();
        static int areaHeights = 0;

        static void Main(string[] args)
        {
            // Number of rows for each area
            areaHeights = (Console.WindowHeight - 2);

            
            // Run through the starting dialog.
            Client.Entry entry = new Client.Entry();
            Client.Entry.ClientRequestInfo UserSettings = entry.FindServer();

            entry.CheckServer(UserSettings);
            DrawScreen(); //initalize the screen
          //  AddLineToBuffer(ref area1, UserSettings.ip_address + "|" + UserSettings.password + "|" + UserSettings.port_number.ToString() + "|" + UserSettings.username);
            
            string Input = "";
            while (true)
            {
                try
                {
                    // Timestamp the Input.
                    Input = Console.ReadLine();
                }
                catch (Exception)
                {
                    // todo: specifiy this exeception
                    throw;
                }
                

                if (Input != "")
                {
                    AddLineToBuffer(ref area1, Input);
                }
                  //  AddLineToBuffer(ref area2, Console.ReadLine());
                DrawScreen();
            }
        }

        private static void AddLineToBuffer(ref List<string> areaBuffer, string line)
        {
            areaBuffer.Insert(0, line);

            if (areaBuffer.Count == areaHeights)
            {
                areaBuffer.RemoveAt(areaHeights - 1);
            }
        }


        private static void DrawScreen()
        {
            Console.Clear();

            // Draw the area divider
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, areaHeights);
                Console.Write('=');
            }

            int currentLine = areaHeights - 1;

            for (int i = 0; i < area1.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));                
                Console.WriteLine(area1[i]);                                
            }

            currentLine = (areaHeights * 2);
            for(int i = 0; i < area2.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));
                Console.WriteLine(area2[i]);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("> ");

        }
    
    }
    
}
