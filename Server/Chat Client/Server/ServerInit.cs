using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Chat_Client.Server
{
	class ServerInit 
	{
		
	

		/* settings of the server */
		public struct ServerSettings
		{
			public int port_number, backlog;
			public string server_ip_address, server_password ,server_name;

			public ServerSettings(int port, int log, string ip, string name, string password)
			{
				port_number = port;
				backlog = log;
				server_ip_address = ip;
				server_name = name;
				server_password = password;
			}
			
		}

		

		/* used to add a new server entry */

		public void Create()
		{
			ServerSettings settings = new ServerSettings();
			ServerList serverlist = new ServerList();

			settings.server_name = GetSeverName();
			settings.server_password = GetServerPassword();
			settings.backlog = GetServerBacklog();
			settings.port_number = GetServerPortNumber();
			settings.server_ip_address = GetServerIPAddress();

			Console.WriteLine(settings.server_name + " server created.");
			CreateNewServer(settings);
		}


			
		/* this method is used when the server starts to search and load settings to be passed to the Listen() method */
		public static ServerSettings Init(string ServerName)
		{
			ServerList SL = new ServerList();
			string[] ServerList = SL.GetServerList(); // list of all directory paths
			string ServerPath = "";
			// ex: ServerList[0] = Server\Caleb
								

			// we want to remove the parent directory from the path so we can retrieve the name
			// for checking
			// then we populate ConfigurationFile with elements of the txt file.
			// we then use a SeverSettings to pass into Listen()
		   
			for (int i = 0; i < ServerList.Length; i++)            
				ServerList[i] = ServerList[i].Remove(0, SL.MainServerDirectory.Length + 1);


			for (int i = 0; i < ServerList.Length; i++)
			{
				if ( ServerName == ServerList[i])
				{
					ServerPath = SL.MainServerDirectory + @"\" + ServerName;
				}
			}

			//gets the configuration file from the Servers\myserver directory
			DirectoryInfo ConfigFileInfo = new DirectoryInfo(ServerPath);
			FileInfo[] files = ConfigFileInfo.GetFiles();
			string ConfigFilePath = "";
			
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i].Extension == ".txt" && files[i].Name.Contains("_Config"))
				{
					ConfigFilePath = files[i].FullName;
				}
   
			}

			
			List<string> ConfigurationFile = Tools.IO.ReadTextFileList(ConfigFilePath);
			ServerSettings settings = new ServerSettings();

			//TODO: could I just iterate through this and add through the structure to make it less hacky?
			settings.server_name        = ConfigurationFile[1];
			settings.server_password    = ConfigurationFile[2];
			settings.backlog            = int.Parse( ConfigurationFile[3] );
			settings.server_ip_address  = ConfigurationFile[4];
			settings.port_number        = int.Parse ( ConfigurationFile[5] );
		
			return settings;
		}       

		
		// add the server to the server list and write the configurations
		private static void CreateNewServer(ServerSettings settings)
		{
			ServerList Servers = new ServerList();
			Servers.Add(settings); // ServerList creates the configuration file and res folder
		}

		// prompts the user to enter in a server name
		private string GetSeverName()
		{
			ServerList serverlist = new ServerList();
			Console.WriteLine("Enter the name of the new server: ");
			while (true)
			{
				string input = Console.ReadLine();

				if (!(string.IsNullOrEmpty(input)))
				{
					if (!(Directory.Exists(serverlist.MainServerDirectory + @"\" + input)))
					{
						return input;
					}
					else
					{
						Console.WriteLine("The server already exists");
					}

				}
				else
				{
					Console.WriteLine("please enter in a name for the server");
				}
			}

		}

		// prompts the user to enter in a password for the server
		private string GetServerPassword()
		{
			while (true)
			{
				Console.WriteLine("Enter in the password for this sever: ");
				string input = Console.ReadLine();

				if (!(string.IsNullOrEmpty(input)))
				{
					return input;                    
				}
				else
				{
					return "";
				}

			}
			

		}

		// prompts the user to enter in a backlog for the server
		private int GetServerBacklog()
		{
			while (true)
			{
				Console.WriteLine("Enter in a backlog for the server:");
				int input = 0;
				try
				{
					input = int.Parse(Console.ReadLine());
					return input;                    
				}
				catch (Exception)
				{
					Console.WriteLine("Invalid Input, please enter in a number \n(hint): try 5");
				}

			}         
		}

		private int GetServerPortNumber()
		{
			while (true)
			{
				Console.WriteLine("Enter the port number.\nif nothing is supplied the port will default to 7777.");
				int input = 0;
				try
				{
					string inp = Console.ReadLine(); // so we can read the input as a string first instead of directly parsing Console.ReadLine().
					if (inp == "")
					{
						return 7777;
					}
					else
					{
						input = int.Parse(inp);
						return input;
					}

				}
				catch (Exception)
				{
					Console.WriteLine("Invalid Input, please enter in a valid port number \n(hint: try 12000");
				}
			}
		}

		private string GetServerIPAddress()
		{
			IPHostEntry host = Dns.Resolve(Dns.GetHostName()); // only need the IP as a string here
			return host.AddressList[0].ToString();
		}

		


	}
}
