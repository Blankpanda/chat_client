using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Client
{
    internal class Client
    {
        public Entry.ClientRequestInfo settings; /* settings for the user to pass in*/

        private static List<string> area1 = new List<string>();
        private static List<string> area2 = new List<string>();

        private static int areaHeights = 0;

        /* constructor requires the user to enter in a strucutre with settings.*/
        private string _ConnectedServerAddress = "";

        private bool _isConnected = false;

        public Client(Entry.ClientRequestInfo userSettings)
        {
            settings = userSettings;
        }

        public string ConnectedServerAddress
        {
            get
            {
                return _ConnectedServerAddress;
            }
            set { ;}
        }

        public bool isConnected
        {
            get
            {
                return _isConnected;
            }
            set { ;}
        }

        // todo: write this
        public void DropConnection()
        {
        }

        public int SendMessage(Socket sender, string msg)
        {
            Chat messenger = new Chat(settings.username);
            byte[] message = Encoding.ASCII.GetBytes(msg + "<EOF>");
            int sent = sender.Send(message);

            return sent;
        }

        public void Start()
        {
            // get the server and its port and connect it to an endpoint

            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP;

            IPAddress ip = IPAddress.Parse(settings.ip_address);
            remoteEP = new IPEndPoint(ip, settings.port_number);

            areaHeights = (Console.WindowHeight -2);

            try
            {
                try
                {
                    sender.Connect(remoteEP);
                    DrawScreen();
                    // AddLineToBuffer(ref area1, "conn " + sender.RemoteEndPoint.ToString());                    
                    while (true)
                    {
                        string Input = Console.ReadLine();
                        SendMessage(sender, settings.username + ": " + Input);

                        byte[] buf = new byte[1024];
                        int BytesRecieved = sender.Receive(buf);
                        string returned = Encoding.ASCII.GetString(buf, 0, BytesRecieved);

                        returned = returned.Replace("<EOF>", "");                        
                       // AddLineToBuffer(ref area1, returned);
                        string[] cont = returned.Split('|');
                        area1 = new List<string>(cont);
                        DrawScreen();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't Connect to server.");
                Environment.Exit(0);
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
            for (int i = 0; i < area2.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));
                Console.WriteLine(area2[i]);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("> ");
        }

        /// <summary>
        /// DEPRACATED
        /// </summary>
        //public void Start()
        //{
        //    try
        //    {
        //        // get the server and its port and connect it to an endpoint
        //        IPAddress ip = IPAddress.Parse(settings.ip_address);
        //        IPEndPoint remoteEP = new IPEndPoint(ip, settings.port_number);

        //        try
        //        {
        //           sender.Connect(remoteEP);

        //           // intially we want to send a message to the server telling what IP is connecting to it
        //          // client.SendIP(sender);

        //           // Begin Chat.
        //           while (true)
        //           {
        //        //       client.SendMessage(sender); // send the message

        //               // get the bytes that we recieve
        //               byte[] buf = new byte[1024];
        //               int BytesRecieved = sender.Receive(buf);

        //               // write out any response we recieve.
        //               string returned = Encoding.ASCII.GetString(buf, 0, BytesRecieved);
        //               returned = returned.Replace("<EOF>", "");
        //               Console.WriteLine(returned);
        //           }

        //           sender.Shutdown(SocketShutdown.Send);
        //        }
        //        catch (Exception)
        //        {
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
    }
}
