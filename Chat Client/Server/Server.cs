﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client
{
    /// <summary>
    /// Listens for connections and makes desicions based on that.
    /// </summary>
    class Server
    {
       
        private ServerInit.ServerSettings settings;
        public static string data = null;

        public Server(ServerInit.ServerSettings ServerConfiguration)
        {
            settings = ServerConfiguration; // the configuration of the server that is being initalzied
        }
        

        // this is used to organzie RunCommand.cs
        public void Start()
        {
                      
            Console.WriteLine(settings.server_name + " started.");

            CommandStructure.RunCommand command = new CommandStructure.RunCommand(); // change the prompt.
            Console.WriteLine();
            command.Prompt(settings.server_name);

           Listen(settings);
        }

        private void Listen(ServerInit.ServerSettings settings)
        {
            byte[] buffer = new byte[1024]; // holds the incoming message.

            //endpoint for the socket.
            IPAddress ip = IPAddress.Parse(settings.server_ip_address);
            IPEndPoint localEndPoint = new IPEndPoint(ip, settings.port_number);

            // basic TCP stream.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            // Bind the socket to the local endpoint
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(settings.backlog);

                while (true)
                {
                    Console.WriteLine("Server is listening.");

                    // another socket handles the data that was returned
                    Socket handler = listener.Accept();

                    while (true)
                    {
                        buffer = new byte[1024];
                        int bytesRc = handler.Receive(buffer);

                        data += Encoding.ASCII.GetString(buffer, 0, bytesRc);
                        if (data.IndexOf("<EOF>") > - 1)
                        {
                            break;
                        }
                    }

                    Console.WriteLine(data);

                    // send it back!
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
