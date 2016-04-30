using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Chat_Client.Server
{
	/// <summary>
	/// Listens for connections and makes desicions based on that.
	/// </summary>
	class Server
	{
	   
		private ServerInit.ServerSettings settings;
		public static string data = null;

	//	public static Hashtable clientList = new Hashtable();

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

			// list of IPs that have connected to the server.
			List<string> IPconnectionsList = new List<string>();

			// clear the contents of the console.
			CommandStructure.Commands.Clear.Execute();


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
					data = ProcessData(handler); // returns the incoming data as a string.

					// if the type is SentIp from the client we want to store the IP and clear
					// data so we can echo actual messages.

					if (data.Contains("type:SentIP"))
					{
						string[] SplitMessage = data.Split(' '); // split the message up. so we can retrieve the IP

						IPconnectionsList.Add(SplitMessage[0]);
						Console.WriteLine(SplitMessage[0] + " Connected.");
					}
					else // return any other message
					{
						// we want to return the data to the client
						byte[] ReturnMessage = Encoding.ASCII.GetBytes(data);
						handler.Send(ReturnMessage);
                        Console.WriteLine();
					}
				   

					
				}
				
			   
			}
			catch (SocketException e)
			{
				if( e.NativeErrorCode == 10035 )
				{
					Console.WriteLine("et");
				}
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
		

		#endregion



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
	}
}
