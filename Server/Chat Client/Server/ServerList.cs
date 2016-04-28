using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Chat_Client.Server
{
	/// <summary>
	/// Used to maintain and add to a list of directories 
	/// </summary>
	class ServerList
	{
		private string ServerDirectory = "Servers";

		public string MainServerDirectory
		{
			get { return ServerDirectory;  }
			set { MainServerDirectory = ServerDirectory; }
		}

		/* Initalizes a directory in the constructor to hold all fo the servers config files */        
		public ServerList()
		{
			Directory.CreateDirectory(ServerDirectory);
		}


		/* Creates a directory to store the configuration file and writes the configuration file */
		internal void Add (ServerInit.ServerSettings settings)
		{
			
			string NewDirectory = ServerDirectory + @"\" + settings.server_name; // EX Servers\myserver\
			Directory.CreateDirectory(NewDirectory);																											
			
			List<string> settings_content = new List<string>();
			// build a list that makes the configuration file

			// Example config file:
			//      server #
			//      name
			//      password
			//      backlog
			//      IP
			//      Port

			int ServerNumber = GetServerListCount(); // gives the server a number


			settings_content.Add(ServerNumber.ToString());  // Server #
			settings_content.Add(settings.server_name);     // Name
			settings_content.Add(settings.server_password); // password
			settings_content.Add(settings.backlog.ToString());  // Backlog
			settings_content.Add(settings.server_ip_address);   // IP
			settings_content.Add(settings.port_number.ToString());  //  Port

			string TextFile = settings.server_name + "_Config" + ".txt";
			string ConfigPath = ServerDirectory + @"\" + settings.server_name + @"\";

			ConfigPath = ConfigPath + TextFile;
		
			Tools.IO.WriteToTextFile(settings_content, ConfigPath);

			
		}

		/* delets a server from the server list */
		public void Delete()
		{
			Console.WriteLine("Enter the name of the server you want to remove.");

			string inp = 
				Console.ReadLine();

			string path = inp; // useless but doccumenting 
			string[] srvs = GetServerList();

			bool exists = false; // I dont really want to do this.

			for (int i = 0; i < srvs.Length; i++)            
				if (path == srvs[i])				
					exists = true;
				

			if (exists)			
			{
				Directory.Delete(ServerDirectory + @"\" +path, true);            
				Console.WriteLine(path + " server deleted.");
			}
			else			
				Console.WriteLine("Sever doesn't exist. type 'SList' to display the lists of servers");
			
			
			
		}
		// The user supplied a name to the delete command and we want to remove it using the name they supplied
		public void Delete(string name)
		{
			name = name.ToLower();

			string path = @"Servers\" + name;
			string[] srvs = GetServerList();

			bool exists = false; // I dont really want to do this.

			for (int i = 0; i < srvs.Length; i++)
				if (path == srvs[i])
					exists = true;


			if (exists)
			{
				Directory.Delete(path, true);
				Console.WriteLine(path + " server deleted.");
			}
			else
				Console.WriteLine("Sever doesn't exist. type 'SList' to display the lists of servers");

		}

		// returns the server list with their config files
		public string[] GetServerList()
		{
			string[] directories = Directory.GetDirectories(ServerDirectory);            
			// remove the parent directory part of each string (we dont need it.)
			
			for (int i = 0; i < directories.Length; i++)
			{
				directories[i] = directories[i].Split('\\')[1];
			
			}

			return directories;
		}

		// displays the server list using standard output
		public void DisplayServerList()
		{
			string[] directories = GetServerList();
			
			if (directories.Length == 0)
			{
				Console.WriteLine("No servers currently created.");
			}
			else
			{

				for (int i = 0; i < directories.Length; i++)
				{
					Console.WriteLine(directories[i]);
				}
			}

		}

		/* Gets the total number of directories in the servers directory */        
		public int GetServerListCount()
		{
			int ServerCount = 0;
			ServerCount = Directory.GetDirectories(ServerDirectory, "*").Length;
			return ServerCount;
		}

		public bool ServerExists(string ServerName)
		{
			// we need to add the Folder that Servers is contained in so we can check for equality.
			
			string[] Servers = GetServerList();

			for (int i = 0; i <= Servers.Length - 1; i++)
				if (ServerName == Servers[i])
					return true;    // the user entry and a entry in the server list matched


			return false;          // the user entry and a entry in the server list didnt match

		}

		internal ServerInit.ServerSettings GetServerByName(string ServerName)
		{         
			// this should always work as long as ServerExists() is used before to check if it has a valid name			
			return ServerInit.Init(ServerName);
		}

		public void PrintServerSettings()
		{
			string[] ids = { "ServerId", "ServerName", "ServerPassword", "ServerBacklog", "ServerIP", "ServerPort" };      
			string[] Servers = GetServerList();
			if (Servers.Length == 0)
				Console.WriteLine("No Servers currently created.");
			else
			{            
				Console.WriteLine("What sever would you like to read?");             
				string Name = Console.ReadLine();


				if (ServerExists(Name))
				{

					for (int i = 0; i <= Servers.Length - 1; i++)
					{
						if (Name == Servers[i])
						{
							string ServerPath = MainServerDirectory + @"\" + Name + @"\" + Name +"_config.txt"; // BLECH
							List<string> ReadSettings = Tools.IO.ReadTextFileList(ServerPath);

							for (int j = 0; j < ReadSettings.Count - 1; j++) // - 1 for newline.
							{
								Console.WriteLine(ids[j] + "=" + ReadSettings[j]);
							}
							break;
						}
					}
				}
				else
				{
					Console.WriteLine("Invalid name for command 'view'");
				}
			}
					
		}

	}
}
