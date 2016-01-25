using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client.CommandStructure.Commands
{
	/// <summary>
	/// starts a supplied server from name.
	/// </summary>
	class Start
	{

		private string _Name = "start";
		private string _Desc = "Starts a server by name";
		public string Name
		{
			get { return _Name; }
			set { Name = _Name; }
		}

		public string Description
		{
			get { return _Desc; }
			set { Description = _Desc; }
		}


		public static void Execute()
		{		
			Server.ServerList InitalizeSettings = new Server.ServerList(); // used to retrieve server settings and test the user input.

			string[] ServerList = InitalizeSettings.GetServerList();    // gets a list of all of the created servers from the Servers/ directory.

			if (ServerList.Length > 0) // does a server exist?
			{
				// Remove @"Servers\" from the strings in the server list.
				// HACK: consider adding to this ServerList.GetServerList(); 
				for (int i = 0; i < ServerList.Length; i++)
					ServerList[i] = ServerList[i].Replace(@"Servers\", "");
					
				


				// Display the server information and ask the user what server they would like to start.
				Console.WriteLine("What server would you like to start (Enter in the number or name)?");
				for (int i = 0; i < ServerList.Length; i++)
				{
					int ServerIndex = i + 1;
					Console.WriteLine(ServerIndex.ToString() + "). " + ServerList[i]);
				}
				Console.WriteLine("------"); // gives the user some space
				string ServerName =
					   Console.ReadLine();

				ServerName = ServerName.ToUpper();

				

				for (int i = 0; i < ServerList.Length; i++)
				{
					int ServerCount = i + 1;
					if (ServerName == ServerCount.ToString() || ServerName == ServerList[i].ToUpper())  // the user can enter the name or a number
					{
						ServerName = ServerList[i]; // if the user enters a number we need to make the input a string of the servers name.
						ServerName = ServerName.Replace(@"Servers\", "");

						Server.ServerInit.ServerSettings Settings = InitalizeSettings.GetServerByName(ServerName); //   load in user settings.               
						Server.Server srv = new Server.Server(Settings);                                          //    create a new object with those server settings
				
						Console.WriteLine(Settings.server_name + " starting.");
				
						srv.Start();     // start the server

					}
					else
					{
						Console.WriteLine("Invalid server name supplied.");						
					}
				} 
			}
			else
			{
				Console.WriteLine("No Servers currently created.");
			}
				
		}

		/*Overload for an argument that already has been supplied*/
		public static void Execute(string ServerName)
		{						

			Server.ServerList InitalizeSettings = new Server.ServerList();

			if (InitalizeSettings.ServerExists(ServerName))
			{
				Server.ServerInit.ServerSettings Settings = InitalizeSettings.GetServerByName(ServerName); //   load in user settings.               
				Server.Server srv = new Server.Server(Settings);                                          //    create a new object with those server settings

				
				Console.WriteLine(Settings.server_name + " starting.");
				
				srv.Start();     // start the server
			}
			else
			{
				Console.WriteLine("Invalid server name supplied.");			
			}

		}
	}
}
