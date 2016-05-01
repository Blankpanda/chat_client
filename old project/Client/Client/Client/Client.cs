using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Client.Client
{
	class Client
	{
		public Entry.ClientRequestInfo settings; /* settings for the user to pass in*/
	  
		private static List<string> area1 = new List<string>();
		private static List<string> area2 = new List<string>();
		
		private static int areaHeights = 0;

		/* constructor requires the user to enter in a strucutre with settings.*/
		public Client(Entry.ClientRequestInfo userSettings)
		{
			settings = userSettings;
		}
		
		private bool _isConnected = false;
		public bool isConnected
		{
			get
			{
				return _isConnected;  
			}
			set { ;}
		}
		
		private string _ConnectedServerAddress = "";
		public string ConnectedServerAddress
		{
			get 
			{                
				return _ConnectedServerAddress; 
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


		public void Start2()
		{
			// get the server and its port and connect it to an endpoint
			
			Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint remoteEP;

			IPAddress ip = IPAddress.Parse(settings.ip_address);
			remoteEP = new IPEndPoint(ip, settings.port_number);

			areaHeights = (Console.WindowHeight - 2);

			try
			{
				try
				{									  				  
				   sender.Connect(remoteEP);                   
				   DrawScreen();
				  // AddLineToBuffer(ref area1, "conn " + sender.RemoteEndPoint.ToString());
				  // DrawScreen();
					while (true)
					{
						string Input = Console.ReadLine();
						SendMessage(sender, Input);												

						byte[] buf = new byte[1024];
						int BytesRecieved = sender.Receive(buf);
						string returned = Encoding.ASCII.GetString(buf, 0, BytesRecieved);

						returned = returned.Replace("<EOF>", ""); 
						AddLineToBuffer(ref area1, returned);
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
				
				throw;
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


		private static void AddLineToBuffer(ref List<string> areaBuffer, string line)
		{
			areaBuffer.Insert(0, line);

			if (areaBuffer.Count == areaHeights)
			{
				areaBuffer.RemoveAt(areaHeights - 1);
			}
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
