using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat_Client.Server
{
    /// <summary>
    /// Listens for connections and makes desicions based on that.
    /// </summary>
    internal class Server
    {
        public Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string data = null;

        private ServerInit.ServerSettings settings;        
        private byte[] bytes = new Byte[1024];

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
            Listen();
        }

        /// <summary>
        /// Listens for a connection.
        /// </summary>
        /// <param name="settings"></param>
        private void Listen()
        {
            // byte[] buffer = new byte[1024]; // holds the incoming message.
            //endpoint for the socket.
            IPAddress ip = IPAddress.Parse(settings.server_ip_address);
            IPEndPoint localEndPoint = new IPEndPoint(ip, settings.port_number);

    
            // clear the contents of the console.
            CommandStructure.Commands.Clear.Execute();

            try
            {
                // Bind the socket to the local endpoint
                listener.Bind(localEndPoint);
                listener.Listen(settings.backlog);

                Console.WriteLine("Server is listening.");

                int bcount = 0;
                while (bcount <= settings.backlog)
                {
                    Thread EstablishConnectionThread = new Thread(NewConnection);    
                    EstablishConnectionThread.Start();

                    bcount++;
                }

                while (true)
                {
                    ;
                }
                
                // TODO: VERY IMPORTATN THREADING < <FDSKLF:LSDKJLF:DSKJL:
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode == 10035)
                {
                    Console.WriteLine("et");
                }
            }
        }

        private void NewConnection()
        {
            Socket handler = listener.Accept();
            Console.WriteLine(handler.RemoteEndPoint.ToString() + " " + "connected.");
           
            List<string> ChatLog = new List<string>();

            while (true)
            {
                // start a thread that handles other connections
                Console.WriteLine("Waiting for Request.");

                data = null;
                int recv;
                while (true)
                {
                    bytes = new byte[1024];
                    try 
                    {
                       recv = handler.Receive(bytes); 
                    }                                       
                    catch (Exception)
                    {
                        continue;
                    }
                        
                    
                    
                    data += Encoding.ASCII.GetString(bytes, 0, recv);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // we want to return the data to the client
                ChatLog.Add(TimeStamp.WriteTimeNoDate(data));
                ChatLog.Reverse();
                string fullLog = "";
                foreach (var text in ChatLog)
                {
                    fullLog += text + "|"; // test delimiter
                }
                ChatLog.Reverse(); // reset it back to the way its suppose to be

                //TimeStamp.WriteTimeNoDate(data)
                byte[] ReturnMessage = Encoding.ASCII.GetBytes(fullLog);
                Console.WriteLine(handler.RemoteEndPoint.ToString() + " says " + TimeStamp.WriteTime(data));
                handler.Send(ReturnMessage);
            }
        }

        #region 2

        //private void Listen2(ServerInit.ServerSettings settings)
        //{
        //    TcpListener ServerSocket = new TcpListener(settings.port_number);
        //    TcpClient ClientSocket = default(TcpClient);
        //    int counter = 0;

        //    ServerSocket.Start();

        //    Console.WriteLine("Server Started.");
        //    counter = 0;
        //    while (true)
        //    {
        //        counter += 1;
        //        ClientSocket = ServerSocket.AcceptTcpClient();

        //        byte[] BytesFrom = new byte[4096];
        //        string ClientData = null;

        //        NetworkStream NetStream = ClientSocket.GetStream();
        //        NetStream.Read(BytesFrom, 0, (int)ClientSocket.ReceiveBufferSize);
        //        ClientData = System.Text.Encoding.ASCII.GetString(BytesFrom);
        //        ClientData = ClientData.Substring(0, ClientData.IndexOf('$'));

        //        clientList.Add(ClientData, ClientSocket);

        //        broadcast(ClientData + " Joined ", ClientData, true);
        //    }

        //    ClientSocket.Close();
        //    ServerSocket.Stop();
        //    Console.WriteLine("exit");
        //    Console.ReadLine();
        //}

        //private void broadcast(string msg, string UserName, bool flag)
        //{
        //    foreach (DictionaryEntry Item in clientList)
        //    {
        //        TcpClient BroadcastSocket;
        //        BroadcastSocket = (TcpClient)Item.Value;
        //        NetworkStream BroadcastStream = BroadcastSocket.GetStream();
        //        Byte[] BroadcastBytes = null;

        //        if (flag == true)
        //        {
        //            string time = TimeStamp.GetTime();
        //            BroadcastBytes = Encoding.ASCII.GetBytes(time + " " + UserName + " said : " + msg);
        //        }
        //        else
        //        {
        //            BroadcastBytes = Encoding.ASCII.GetBytes(msg);
        //        }
        //        BroadcastStream.Write(BroadcastBytes, 0, BroadcastBytes.Length);
        //        BroadcastStream.Flush();
        //    }
        //}

        /// <summary>
        /// Reads incoming data and stores it in a buffer which then formats it into a string.
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

        #endregion 2     
    }
}