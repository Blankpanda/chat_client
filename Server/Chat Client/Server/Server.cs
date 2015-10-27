using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat_Client.Server
{
    /// <summary>
    /// Listens for connections and makes desicions based on that.
    /// </summary>
    class Server
    {
       
        private ServerInit.ServerSettings settings;
        public static string data = null;
        
    
        /// <summary>
        /// Constructor takes Server configuration structure which is read from the config file.
        /// </summary>
        /// <param name="ServerConfiguration"></param>
        public Server(ServerInit.ServerSettings ServerConfiguration)
        {
            settings = ServerConfiguration; // the configuration of the server that is being initalzied
        }
        

      
        /// <summary>
        ///  Organizes the Start prompt and begins listening by passing in the settings.
        /// </summary>
        public void Start()
        {
                      
            Console.WriteLine(settings.server_name + " started.");

            CommandStructure.RunCommand command = new CommandStructure.RunCommand(); // change the prompt.
            Console.WriteLine();
            command.Prompt(settings.server_name + "> ");
            
            Listen(settings);
        }

        /// <summary>
        /// Listens for a connection.
        /// </summary>
        /// <param name="settings"></param>
        private void Listen(ServerInit.ServerSettings settings)
        {
            // byte[] buffer = new byte[1024]; // holds the incoming message.

            //endpoint for the socket.
            IPAddress ip = IPAddress.Parse(settings.server_ip_address);
            IPEndPoint localEndPoint = new IPEndPoint(ip, settings.port_number);

            // basic TCP stream.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            // Bind the socket to the local endpoint
            try
            {
                // starts listening for incoming connections,
                listener.Bind(localEndPoint);
                listener.Listen(settings.backlog);

                // another socket handles the data that was sent in and retireved by the listener
                Console.WriteLine("Server is listening.");
                Socket handler = listener.Accept();
                    
                while (true)
                {
                    Console.WriteLine("Server is listening.");
                    
                    data = ProcessData(handler); // returns the incoming data as a string.
                    Console.WriteLine(data);

                    if (Console.ReadLine() == "END")
                        handler.Shutdown(SocketShutdown.Both);
                }
                
               
            }
            catch (Exception)
            {
                throw;   
            }

        }

        /// <summary>
        /// Reads incoming data and stores it in a buffer which then
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private string ProcessData(Socket handler)
        {
            string inData = "";
            byte[] buffer = new byte[handler.SendBufferSize];

            while (true)
            {
                buffer = new byte[1024];
                int bytesRc = handler.Receive(buffer); // the number of bytes we recieve               

                inData += Encoding.ASCII.GetString(buffer, 0, bytesRc);

                if (inData.IndexOf("<EOF>") > -1)                
                    break;
                               
            }
            return inData;
        }
    }
}
