﻿using System;
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

		private string _Name = "Start";
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

				// Display the server information and ask the user what server they would like to start.
				Console.WriteLine("What server would you like to start?");
				for (int i = 0; i < ServerList.Length; i++)
					Console.WriteLine(i.ToString() + "). " + ServerList[i]);
				string ServerName =
					   Console.ReadLine();

				ServerName = ServerName.ToUpper();

				Server.Logger EventLogger = new Server.Logger(Server.LogType.Type.EVENT, ServerName); // Set up loggers.


				for (int i = 0; i < ServerList.Length; i++)
				{
					if (ServerName == i.ToString() || ServerName == ServerList[i].ToUpper())  // the user can enter the name or a number
					{
						if (InitalizeSettings.ServerExists(ServerName))
						{
							Server.ServerInit.ServerSettings Settings = InitalizeSettings.GetServerByName(ServerName); //   load in user settings.               
							Server.Server srv = new Server.Server(Settings);                                       //    create a new object with those server settings


							EventLogger.Write(ServerName + " settings are loaded.");

							Console.WriteLine(Settings.server_name + " starting.");
							EventLogger.Write(Settings.server_name + " started.");
							srv.Start();     // start the server

						}
						else
						{
							Console.WriteLine("Invalid server name supplied.");
							EventLogger.Write(ServerName + "is an invalid name.  Server was not found.");
						}
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
			// Set up loggers.


			Server.Logger EventLogger = new Server.Logger(Server.LogType.Type.EVENT, ServerName);


			Server.ServerList InitalizeSettings = new Server.ServerList();


			if (InitalizeSettings.ServerExists(ServerName))
			{
				Server.ServerInit.ServerSettings Settings = InitalizeSettings.GetServerByName(ServerName); //   load in user settings.               
				Server.Server srv = new Server.Server(Settings);                                          //    create a new object with those server settings

				EventLogger.Write(ServerName + " settings are loaded.");

				Console.WriteLine(Settings.server_name + " starting.");
				EventLogger.Write(Settings.server_name + " started.");
				srv.Start();     // start the server
			}
			else
			{
				Console.WriteLine("Invalid server name supplied.");
				EventLogger.Write(ServerName + "is an invalid name.  Server was not found.");
			}

		}
	}
}
